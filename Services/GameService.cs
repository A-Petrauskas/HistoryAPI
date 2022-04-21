using AutoMapper;
using Services.Contracts;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services
{
    public class GameService : IGameService
    {
        private readonly ILevelsService _levelsService;
        private readonly IMapper _mapper;

        private static List<GameInstanceContract> gameList = new List<GameInstanceContract>();

        public GameService(ILevelsService levelsService, IMapper mapper)
        {
            _levelsService = levelsService;
            _mapper = mapper;
        }


        public async Task<GameStartContract> StartNewGameAsync(string levelId)
        {
            var level = await _levelsService.GetLevelNoBCAsync(levelId);

            if (level == null)
            {
                return null;
            }

            var gameInstance = new GameInstanceContract
            {
                gameId = Guid.NewGuid(),
                levelEventsLeft = level.events,
                levelid = level.Id,
                usedEvents = new List<EventContract>(),
                mistakesAllowed = level.mistakes,
                mistakenEvents = new List<EventContract>(),
                lastGameStateSent = new GameStateContract() { gameStatus = EnumGameStatus.stillPlaying },
                fullDates = level.fullDates
            };

            gameList.Add(gameInstance);

            var gameStartContract = new GameStartContract()
            {
                gameId = gameInstance.gameId.ToString(),
                timeConstraint = level.timeConstraint,
                mistakesAllowed = level.mistakes
            };

            return gameStartContract;
        }


        public GameInstanceContract CheckGameExists(string gameId)
        {
            var game = gameList.Find(game => game.gameId.ToString() == gameId);

            if (game == null)
            {
                return null;
            }

            return game;
        }


        public GameStateContract MakeGuessAsync(GameInstanceContract game, int placementIndex)
        {
            // Time constraint checks
            var timeCheckedState = CheckTimeConstraint(game, placementIndex);

            if (timeCheckedState.gameStatus == EnumGameStatus.lost)
            {
                game.lastGameStateSent = timeCheckedState;

                return timeCheckedState;
            }


            // Placement correctness checks
            var eventList = game.levelEventsLeft;

            if (!IsPlacementCorrect(game, placementIndex))
            {
                // Mistake limit checks
                var mistakeCountedState = CheckMistakeCount(game);
                if (mistakeCountedState.gameStatus == EnumGameStatus.lost)
                {
                    game.lastGameStateSent = mistakeCountedState;

                    return mistakeCountedState;
                }

                game.lastGameStateSent.mistakes = game.mistakes;

                return game.lastGameStateSent;
            }


            // Won game check
            if (eventList.Count == 0)
            {
                var gameState = new GameStateContract { gameStatus = EnumGameStatus.won };

                game.lastGameStateSent = gameState;

                return gameState;
            }


            var nextEventGameState = GenerateNewEvent(game, EnumFirstTwoEvents.others);

            return nextEventGameState;
        }


        public GameOverStatsContract GetGameOverStats(GameInstanceContract game)
        {
            var eventsWithBC = ChangeEventDatesToBC(game);

            var gameOverStats = new GameOverStatsContract
            {
                mistakes = game.mistakes,
                mistakenEvents = eventsWithBC
            };

            return gameOverStats;
        }


        public bool IsPlacementCorrect(GameInstanceContract game, int placementIndex)
        {
            if (game.fullDates)
            {
                return IsPlacementCorrectFullDates(game, placementIndex);
            }

            return IsPlacementCorrectYearOnly(game, placementIndex);
        }


        public GameStateContract CheckMistakeCount(GameInstanceContract game)
        {
            if (game.mistakes >= game.mistakesAllowed)
            {
                return new GameStateContract { gameStatus = EnumGameStatus.lost, mistakes = game.mistakes };
            }

            return game.lastGameStateSent;
        }


        public GameStateContract CheckTimeConstraint(GameInstanceContract game, int placementIndex)
        {
            var gameStatus = EnumGameStatus.stillPlaying;

            if (placementIndex == -1)
            {
                gameStatus = EnumGameStatus.lost;
            }

            var gameState = new GameStateContract
            {
                gameStatus = gameStatus,
                mistakes = game.mistakes
            };

            return gameState;
        }


        public GameStateContract GenerateNewEvent(GameInstanceContract game, EnumFirstTwoEvents firstTwoEvents)
        {
            var eventList = game.levelEventsLeft;

            var index = new Random().Next(eventList.Count);

            var nextEvent = eventList[index];

            eventList.RemoveAt(index);
            game.levelEventsLeft = eventList;


            var nextEventGameState = _mapper.Map<GameStateContract>(nextEvent);
            nextEventGameState.mistakes = game.mistakes;


            if (firstTwoEvents == EnumFirstTwoEvents.baseEvent)
            {
                game.usedEvents.Add(nextEvent);
            }


            game.lastGameStateSent = nextEventGameState;
            game.lastEventContractSent = nextEvent;

            return nextEventGameState;
        }


        public bool IsPlacementCorrectYearOnly(GameInstanceContract game, int placementIndex)
        {
            var placedEvents = new List<EventContract>(game.usedEvents);
            var userPlacedEvent = game.lastEventContractSent;

            placedEvents.Add(userPlacedEvent);

            var sortedEvents = placedEvents.OrderBy(o => Int32.Parse(o.date)).ToList();

            // Check if its in the correct position
            if (sortedEvents.IndexOf(userPlacedEvent) == placementIndex)
            {
                game.usedEvents = sortedEvents;

                return true;
            }

            // Check if two items have the same date (relative order is not kept) 
            var unsortedEvents = new List<EventContract>(game.usedEvents);
            unsortedEvents.Insert(placementIndex, game.lastEventContractSent);

            if (placementIndex == 0)
            {
                var eventRightDate = Int32.Parse(unsortedEvents[1].date);
                if (eventRightDate == Int32.Parse(userPlacedEvent.date))
                {
                    game.usedEvents = sortedEvents;
                    return true;
                }

            }

            else if (placementIndex == placedEvents.Count - 1)
            {
                var eventLeftDate = Int32.Parse(unsortedEvents[placementIndex - 1].date);
                if (eventLeftDate == Int32.Parse(userPlacedEvent.date))
                {
                    game.usedEvents = sortedEvents;
                    return true;
                }
            }

            else
            {
                var eventLeftDateMiddle = Int32.Parse(unsortedEvents[placementIndex - 1].date);
                var eventRightDateMiddle = Int32.Parse(unsortedEvents[placementIndex + 1].date);

                if (eventLeftDateMiddle == Int32.Parse(userPlacedEvent.date)
                    || eventRightDateMiddle == Int32.Parse(userPlacedEvent.date))
                {
                    game.usedEvents = sortedEvents;
                    return true;
                }

            }


            game.mistakes++;

            if (!game.mistakenEvents.Contains(userPlacedEvent))
            {
                game.mistakenEvents.Add(userPlacedEvent);
            }

            return false;
        }


        public bool IsPlacementCorrectFullDates(GameInstanceContract game, int placementIndex)
        {
            var placedEvents = new List<EventContract>(game.usedEvents);
            var userPlacedEvent = game.lastEventContractSent;

            placedEvents.Add(userPlacedEvent);

            var sortedEvents = placedEvents.OrderBy(o => DateTime.Parse(o.date)).ToList();

            // Check if its in the correct position
            if (sortedEvents.IndexOf(userPlacedEvent) == placementIndex)
            {
                game.usedEvents = sortedEvents;

                return true;
            }

            // Check if two items have the same date (relative order is not kept) 
            var unsortedEvents = new List<EventContract>(game.usedEvents);
            unsortedEvents.Insert(placementIndex, game.lastEventContractSent);

            if (placementIndex == 0)
            {
                var eventRightDate = DateTime.Parse(unsortedEvents[1].date);
                if (eventRightDate == DateTime.Parse(userPlacedEvent.date))
                {
                    game.usedEvents = sortedEvents;
                    return true;
                }

            }

            else if (placementIndex == placedEvents.Count - 1)
            {
                var eventLeftDate = DateTime.Parse(unsortedEvents[placementIndex - 1].date);
                if (eventLeftDate == DateTime.Parse(userPlacedEvent.date))
                {
                    game.usedEvents = sortedEvents;
                    return true;
                }
            }

            else
            {
                var eventLeftDateMiddle = DateTime.Parse(unsortedEvents[placementIndex - 1].date);
                var eventRightDateMiddle = DateTime.Parse(unsortedEvents[placementIndex + 1].date);

                if (eventLeftDateMiddle == DateTime.Parse(userPlacedEvent.date)
                    || eventRightDateMiddle == DateTime.Parse(userPlacedEvent.date))
                {
                    game.usedEvents = sortedEvents;
                    return true;
                }

            }


            game.mistakes++;

            if (!game.mistakenEvents.Contains(userPlacedEvent))
            {
                game.mistakenEvents.Add(userPlacedEvent);
            }

            return false;
        }


        public List<EventContract> ChangeEventDatesToBC(GameInstanceContract game)
        {
            if (game.fullDates)
            {
                return game.mistakenEvents;
            }

            var eventsWithBC = new List<EventContract>(game.mistakenEvents);

            foreach (EventContract levelEvent in eventsWithBC)
            {
                if (levelEvent.date.Contains('-'))
                {
                    levelEvent.date = levelEvent.date.Remove(0, 1) + " BC";
                }
            }

            return eventsWithBC;
        }
    }
}

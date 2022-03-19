using AutoMapper;
using Services.Contracts;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

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


        public async Task<string> StartNewGameAsync(string levelId)
        {
            var level = await _levelsService.GetLevelAsync(levelId);

            var gameInstance = new GameInstanceContract
            {
                gameId = Guid.NewGuid(),
                levelEventsLeft = level.Events,
                levelid = level.Id,
                usedEvents = new List<EventContract>(),
                mistakesAllowed = level.mistakes,
                mistakenEvents = new List<EventContract>()
            };

            gameList.Add(gameInstance);

            return gameInstance.gameId.ToString();
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


        public GameState MakeGuessAsync(GameInstanceContract game, int placementIndex)
        {
            // Time requirement checks
            var timeCheckedState = CheckTimeConstraint(game);

            if (timeCheckedState.gameStatus == EnumGameStatus.lost)
            {
                return timeCheckedState;
            }


            // Placement correctness checks
            var eventList = game.levelEventsLeft;

            if (!isPlacementCorrect(game, placementIndex))
            {
                // Mistake limit checks
                var mistakeCountedState = CheckMistakeCount(game);
                if(mistakeCountedState.gameStatus == EnumGameStatus.lost)
                {
                    mistakeCountedState.mistakes++;

                    return mistakeCountedState;
                }

                game.lastGameStateSent.mistakes++;

                return game.lastGameStateSent;
            }


            // Finished game check
            if (eventList.Count == 0)
            {
                return new GameState { gameStatus = EnumGameStatus.won };
            }


            var nextEventGameState = GenerateNewEvent(game, EnumFirstTwoEvents.others);

            return nextEventGameState;
        }


        private GameState CheckMistakeCount (GameInstanceContract game)
        {
            if (game.mistakes >= game.mistakesAllowed)
            {
                return new GameState { gameStatus = EnumGameStatus.lost };
            }

            return game.lastGameStateSent;
        }


        private bool isPlacementCorrect(GameInstanceContract game, int placementIndex)
        {
            var placedEvents = new List<EventContract>(game.usedEvents);
            var userPlacedEvent = game.lastEventContractSent;

            placedEvents.Add(userPlacedEvent);

            var sortedEvents = placedEvents.OrderBy(o => o.date).ToList();

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
                var eventRightDate = unsortedEvents[1].date;
                if (eventRightDate == userPlacedEvent.date)
                {
                    game.usedEvents = sortedEvents;
                    return true;
                }
                
            }

            else if (placementIndex == placedEvents.Count - 1)
            {
                var eventLeftDate = unsortedEvents[placementIndex - 1].date;
                if (eventLeftDate == userPlacedEvent.date)
                {
                    game.usedEvents = sortedEvents;
                    return true;
                }
            }

            else
            {
                var eventLeftDateMiddle = sortedEvents[placementIndex - 1].date;
                var eventRightDateMiddle = sortedEvents[placementIndex + 1].date;

                if (eventLeftDateMiddle == userPlacedEvent.date || eventRightDateMiddle == userPlacedEvent.date)
                {
                    game.usedEvents = sortedEvents;
                    return true;
                }
                
            }

            //TODO: ONLY ADD UNIQUE MISTAKENEVENTS!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            game.mistakes++;
            game.mistakenEvents.Add(userPlacedEvent);

            return false;
        }


        private GameState CheckTimeConstraint(GameInstanceContract game)
        {
            var gameState = new GameState
            {
                gameStatus = EnumGameStatus.stillPlaying
            };

            return gameState;
        }


        public GameState GenerateNewEvent(GameInstanceContract game, EnumFirstTwoEvents firstTwoEvents)
        {
            var eventList = game.levelEventsLeft;

            var index = new Random().Next(eventList.Count);

            var nextEvent = eventList[index];

            eventList.RemoveAt(index);
            game.levelEventsLeft = eventList;


            var nextEventGameContract = _mapper.Map<GameState>(nextEvent);


            if (firstTwoEvents == EnumFirstTwoEvents.baseEvent)
            {
                game.usedEvents.Add(nextEvent);
            }


            game.lastGameStateSent = nextEventGameContract;
            game.lastEventContractSent = nextEvent;

            return nextEventGameContract;
        }
    }
}

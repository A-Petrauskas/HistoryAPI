﻿using AutoMapper;
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
            var level = await _levelsService.GetLevelAsync(levelId);

            var gameInstance = new GameInstanceContract
            {
                gameId = Guid.NewGuid(),
                levelEventsLeft = level.Events,
                levelid = level.Id,
                usedEvents = new List<EventContract>(),
                mistakesAllowed = level.mistakes,
                mistakenEvents = new List<EventContract>(),
                lastGameStateSent = new GameState() { gameStatus = EnumGameStatus.stillPlaying }
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


        public GameState MakeGuessAsync(GameInstanceContract game, int placementIndex)
        {
            // Time constraint checks
            var timeCheckedState = CheckTimeConstraint(placementIndex);

            if (timeCheckedState.gameStatus == EnumGameStatus.lost)
            {
                game.lastGameStateSent = timeCheckedState;

                return timeCheckedState;
            }


            // Placement correctness checks
            var eventList = game.levelEventsLeft;

            if (!isPlacementCorrect(game, placementIndex))
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
                var gameState = new GameState { gameStatus = EnumGameStatus.won };

                game.lastGameStateSent = gameState;

                return gameState;
            }


            var nextEventGameState = GenerateNewEvent(game, EnumFirstTwoEvents.others);

            return nextEventGameState;
        }


        public GameOverStatsContract GetGameOverStats(GameInstanceContract game)
        {
            var gameOverStats = new GameOverStatsContract
            {
                mistakes = game.mistakes,
                mistakenEvents = game.mistakenEvents
            };

            return gameOverStats;
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
                var eventLeftDateMiddle = unsortedEvents[placementIndex - 1].date;
                var eventRightDateMiddle = unsortedEvents[placementIndex + 1].date;

                if (eventLeftDateMiddle == userPlacedEvent.date || eventRightDateMiddle == userPlacedEvent.date)
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

        private GameState CheckMistakeCount(GameInstanceContract game)
        {
            if (game.mistakes >= game.mistakesAllowed)
            {
                return new GameState { gameStatus = EnumGameStatus.lost, mistakes = game.mistakes };
            }

            return game.lastGameStateSent;
        }


        private GameState CheckTimeConstraint(int placementIndex)
        {
            var gameStatus = EnumGameStatus.stillPlaying;

            if (placementIndex == -1)
            {
                gameStatus = EnumGameStatus.lost;
            }

            var gameState = new GameState
            {
                gameStatus = gameStatus
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


            var nextEventGameState = _mapper.Map<GameState>(nextEvent);
            nextEventGameState.mistakes = game.mistakes;


            if (firstTwoEvents == EnumFirstTwoEvents.baseEvent)
            {
                game.usedEvents.Add(nextEvent);
            }


            game.lastGameStateSent = nextEventGameState;
            game.lastEventContractSent = nextEvent;

            return nextEventGameState;
        }
    }
}

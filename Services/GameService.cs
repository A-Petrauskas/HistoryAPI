using AutoMapper;
using Services.Contracts;
using Services.Interfaces;
using System;
using System.Collections.Generic;
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


        public async Task<string> StartNewGameAsync(string levelId)
        {
            var level = await _levelsService.GetLevelAsync(levelId);

            GameInstanceContract gameInstance = new GameInstanceContract
            {
                gameId = Guid.NewGuid(),
                levelEvents = level.Events,
                levelid = level.Id,
                usedEvents = new List<EventContract>()
            };

            gameList.Add(gameInstance);
            //Immediately send new event !!!!!!!!!!!!
            return gameInstance.gameId.ToString();
        }


        public EventGameContract GetNextEvent(GameInstanceContract game)
        {
            var eventList = game.levelEvents;

            if (eventList.Count == 0)
            {
                return null;
            }

            var index = new Random().Next(eventList.Count);

            var nextEvent = eventList[index];
            game.usedEvents.Add(nextEvent); //SORT before finding if it is wrong

            eventList.RemoveAt(index);
            game.levelEvents = eventList;

            var nextEventGameContract = _mapper.Map<EventGameContract>(nextEvent);

            return nextEventGameContract;
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
    }
}

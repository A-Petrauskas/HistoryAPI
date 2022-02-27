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

        private static List<GameInstance> gameList = new List<GameInstance>();

        public GameService(ILevelsService levelsService, IMapper mapper)
        {
            _levelsService = levelsService;
            _mapper = mapper;
        }


        public async Task<string> StartNewGameAsync(string levelId)
        {
            var level = await _levelsService.GetLevelAsync(levelId);

            GameInstance gameInstance = new GameInstance
            {
                gameId = Guid.NewGuid(),
                level = level,
                levelid = level.Id
            };

            gameList.Add(gameInstance);

            return gameInstance.gameId.ToString();
        }

        public EventGameContract GetNextEventAsync(string gameId)
        {
            var game = gameList.Find(game => game.gameId.ToString() == gameId);
            var eventList = game.level.Events;

            var index = new Random().Next(eventList.Count);

            var nextEvent = eventList[index];

            eventList.RemoveAt(index); // TODO: CHECK IF ITS OVER (THE LIST IS EMPTY)
            game.level.Events = eventList;

            var nextEventGameContract = _mapper.Map<EventGameContract>(nextEvent);

            return nextEventGameContract; // TODO: remove ids to events in the game but save what event it gave
        }
    }
}

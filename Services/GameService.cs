using Services.Contracts;
using Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace Services
{
    public class GameService : IGameService
    {
        private readonly ILevelsService _levelsService;

        public GameService(ILevelsService levelsService)
        {
            _levelsService = levelsService;
        }


        public async Task<string> StartNewGameAsync(string levelId)
        {
            GameInstance gameInstance = new GameInstance
            {
                gameId = Guid.NewGuid(),
                level = await _levelsService.GetLevelAsync(levelId)
            };

            return gameInstance.gameId.ToString();
        }

        public async Task<EventGameContract> GetNextEventAsync(string gameId)
        {
            //Keep connection alive / listen
            return null;
        }
    }
}

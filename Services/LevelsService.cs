using Repositories;
using Repositories.Entities;
using Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services
{
    public class LevelsService : ILevelsService
    {
        private readonly ILevelsRepository _levelsRepository;

        public LevelsService(ILevelsRepository levelsRepository)
        {
            _levelsRepository = levelsRepository;
        }

        public async Task<List<Level>> GetLevelsAsync()
        {
            var allLevels = await _levelsRepository.GetAllAsync();

            return allLevels;
        }

        public async Task<Level> GetLevelAsync(string id)
        {
            var level = await _levelsRepository.GetAsync(id);

            return level;
        }
    }
}

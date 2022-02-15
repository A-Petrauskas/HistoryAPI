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

        public async Task<List<Level>> GetAllEventsAsync()
        {
            var allEvents = await _levelsRepository.GetAsync();

            return allEvents;
        }
    }
}

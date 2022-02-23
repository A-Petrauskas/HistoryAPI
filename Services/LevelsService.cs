using AutoMapper;
using Repositories;
using Services.Contracts;
using Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services
{
    public class LevelsService : ILevelsService
    {
        private readonly ILevelsRepository _levelsRepository;
        private readonly IMapper _mapper;

        public LevelsService(ILevelsRepository levelsRepository, IMapper mapper)
        {
            _levelsRepository = levelsRepository;
            _mapper = mapper;
        }

        public async Task<List<LevelContract>> GetLevelsAsync()
        {
            var allLevelsEntity = await _levelsRepository.GetAllAsync();

            var allLevelsContract = _mapper.Map<List<LevelContract>>(allLevelsEntity);

            return allLevelsContract;
        }

        public async Task<LevelContract> GetLevelAsync(string id)
        {
            var levelEntity = await _levelsRepository.GetAsync(id);

            var levelContract = _mapper.Map<LevelContract>(levelEntity);

            return levelContract;
        }
    }
}

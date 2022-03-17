using AutoMapper;
using MongoDB.Driver;
using Repositories;
using Repositories.Entities;
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
        private readonly IEventsService _eventsService;

        public LevelsService(ILevelsRepository levelsRepository, IMapper mapper, IEventsService eventsService)
        {
            _levelsRepository = levelsRepository;
            _eventsService = eventsService;
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

        public async Task<LevelContract> GetLevelByNameAsync(string name)
        {
            var levelEntity = await _levelsRepository.GetByNameAsync(name);

            var levelContract = _mapper.Map<LevelContract>(levelEntity);

            return levelContract;
        }

        public async Task<LevelContract> CreateLevelAsync(LevelContract newLevel)
        {
            var newLevelEntity = _mapper.Map<LevelEntity>(newLevel);

            await _levelsRepository.CreateLevelAsync(newLevelEntity);

            await _eventsService.CreateEventsAsync(newLevelEntity.Events);

            var newLevelContract = _mapper.Map<LevelContract>(newLevelEntity);

            return newLevelContract;
        }

        public async Task<LevelContract> UpdateLevelAsync(LevelContract level)
        {
            var levelEntity = _mapper.Map<LevelEntity>(level);

            await _levelsRepository.UpdateLevelAsync(levelEntity);

            return level;
        }

        public async Task<DeleteResult> RemoveLevelAsync(string id)
        {
            var deleteResult = await _levelsRepository.RemoveLevelAsync(id);

            return deleteResult;
        }
    }
}

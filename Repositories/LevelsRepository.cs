using MongoDB.Driver;
using Repositories.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repositories
{
    public class LevelsRepository : ILevelsRepository
    {
        public readonly IMongoCollection<LevelEntity> _levels;

        public LevelsRepository(IHistoryApiDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _levels = database.GetCollection<LevelEntity>("levels");
        }

        public async Task<List<LevelEntity>> GetAllAsync()
        {
            var task = await _levels.FindAsync<LevelEntity>(_ => true);

            return await task.ToListAsync();
        }


        public async Task<LevelEntity> GetAsync(string id)
        {
            var task = await _levels.FindAsync<LevelEntity>(level => level.Id == id);

            return await task.FirstOrDefaultAsync();
        }

        public async Task<LevelEntity> GetByNameAsync(string name)
        {
            var task = await _levels.FindAsync(level => level.name == name);

            return await task.FirstOrDefaultAsync();
        }

        public async Task<LevelEntity> CreateLevelAsync(LevelEntity newLevel)
        {
            await _levels.InsertOneAsync(newLevel);

            return newLevel;
        }

        public async Task<LevelEntity> UpdateLevelAsync(LevelEntity level)
        {
            var updatedLevel = await _levels.ReplaceOneAsync(l => l.Id == level.Id, level);

            return level;
        }

        public async Task<DeleteResult> RemoveLevelAsync(string levelId)
        {
            var deleteResult = await _levels.DeleteOneAsync(level => level.Id == levelId);

            return deleteResult;
        }
    }
}

using MongoDB.Driver;
using Repositories.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repositories
{
    public interface ILevelsRepository
    {
        Task<List<LevelEntity>> GetAllAsync();

        Task<LevelEntity> GetAsync(string id);

        Task<LevelEntity> GetByNameAsync(string name);

        Task<LevelEntity> CreateLevelAsync(LevelEntity newLevel);

        Task<LevelEntity> UpdateLevelAsync(LevelEntity level);

        Task<DeleteResult> RemoveLevelAsync(string levelId);
    }
}

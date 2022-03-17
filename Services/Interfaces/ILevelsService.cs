using MongoDB.Driver;
using Services.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface ILevelsService
    {
        Task<List<LevelContract>> GetLevelsAsync();

        Task<LevelContract> GetLevelAsync(string id);

        Task<LevelContract> GetLevelByNameAsync(string name);

        Task<LevelContract> CreateLevelAsync(LevelContract newLevel);

        Task<LevelContract> UpdateLevelAsync(LevelContract level);

        Task<DeleteResult> RemoveLevelAsync(string id);
    }
}

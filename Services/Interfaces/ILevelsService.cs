using Services.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface ILevelsService
    {
        Task<List<LevelContract>> GetLevelsAsync();

        Task<LevelContract> GetLevelAsync(string id);
    }
}

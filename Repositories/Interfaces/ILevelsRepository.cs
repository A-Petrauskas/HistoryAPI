using Repositories.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repositories
{
    public interface ILevelsRepository
    {
        Task<List<Level>> GetAsync();
    }
}

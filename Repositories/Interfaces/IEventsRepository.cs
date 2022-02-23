using Repositories.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IEventsRepository
    {
        Task<List<EventEntity>> GetAllAsync();

        Task<EventEntity> GetAsync(string id);
    }
}

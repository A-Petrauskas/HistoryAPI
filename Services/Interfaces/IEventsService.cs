using Repositories.Entities;
using Services.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IEventsService
    {
        Task<List<EventContract>> GetEventsAsync();

        Task<EventContract> GetEventAsync(string id);

        Task<List<EventEntity>> CreateEventsAsync(List<EventEntity> eventsToCreate);
    }
}

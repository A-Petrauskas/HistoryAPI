using Repositories.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IEventService
    {
        Task<List<Event>> GetAllEventsAsync();
    }
}

using Repositories.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IEventsService
    {
        Task<List<Event>> GetAllEventsAsync();
    }
}

using Repositories;
using Repositories.Models;
using Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services
{
    public class EventService : IEventService
    {
        private readonly IEventsRepository _eventsRepository;

        public EventService(IEventsRepository eventsRepository)
        {
            _eventsRepository = eventsRepository;
        }

        public async Task<List<Event>> GetAllEventsAsync()
        {
            var allEvents = await _eventsRepository.GetAsync();

            return allEvents;
        }
    }
}

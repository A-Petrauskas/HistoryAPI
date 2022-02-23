using Repositories;
using Repositories.Entities;
using Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services
{
    public class EventsService : IEventsService
    {
        private readonly IEventsRepository _eventsRepository;

        public EventsService(IEventsRepository eventsRepository)
        {
            _eventsRepository = eventsRepository;
        }

        public async Task<List<Event>> GetEventsAsync()
        {
            var allEvents = await _eventsRepository.GetAllAsync();

            return allEvents;
        }

        public async Task<Event> GetEventAsync(string id)
        {
            var historyEvent = await _eventsRepository.GetAsync(id);

            return historyEvent;
        }
    }
}

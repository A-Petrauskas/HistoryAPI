using AutoMapper;
using Repositories;
using Repositories.Entities;
using Services.Contracts;
using Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services
{
    public class EventsService : IEventsService
    {
        private readonly IEventsRepository _eventsRepository;
        private readonly IMapper _mapper;

        public EventsService(IEventsRepository eventsRepository, IMapper mapper)
        {
            _eventsRepository = eventsRepository;
            _mapper = mapper;
        }

        public async Task<List<EventContract>> GetEventsAsync()
        {
            var allEventsEntity = await _eventsRepository.GetAllAsync();

            var allEventsContract = _mapper.Map<List<EventContract>>(allEventsEntity);

            var allEventsWithBC = ChangeEventDatesToBC(allEventsContract);

            return allEventsWithBC;
        }

        public async Task<EventContract> GetEventAsync(string id)
        {
            var historyEventEntity = await _eventsRepository.GetAsync(id);

            var historyEventContract = _mapper.Map<EventContract>(historyEventEntity);

            return historyEventContract;
        }

        public async Task<List<EventEntity>> CreateEventsAsync(List<EventEntity> eventsToCreate)
        {
            await _eventsRepository.CreateEventsAsync(eventsToCreate);

            return eventsToCreate;
        }


        public List<EventContract> ChangeEventDatesToBC(List<EventContract> events)
        {
            var eventsWithBC = new List<EventContract>(events);

            foreach (EventContract levelEvent in eventsWithBC)
            {
                if (levelEvent.date[0] == '-')
                {
                    levelEvent.date = levelEvent.date.Remove(0, 1) + " BC";
                }
            }

            return eventsWithBC;
        }
    }
}

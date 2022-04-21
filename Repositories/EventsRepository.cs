using MongoDB.Driver;
using Repositories.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repositories
{
    public class EventsRepository : IEventsRepository
    {
        public readonly IMongoCollection<EventEntity> _events;

        public EventsRepository(IHistoryApiDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _events = database.GetCollection<EventEntity>("events");
        }

        public async Task<List<EventEntity>> GetAllAsync()
        {
            var task = await _events.FindAsync<EventEntity>(_ => true);

            return await task.ToListAsync();
        }

        public async Task<EventEntity> GetAsync(string id)
        {
            var task = await _events.FindAsync(historyEvent => historyEvent.Id == id);

            return await task.FirstOrDefaultAsync();
        }

        public async Task<List<EventEntity>> CreateEventsAsync(List<EventEntity> eventsToCreate)
        {
            var task = await _events.FindAsync<EventEntity>(_ => true);

            var eventsInDB = await task.ToListAsync();

            for (int i = 0; i < eventsToCreate.Count; i++)
            {
                if (!eventsInDB.Any(evnt => evnt.description == eventsToCreate[i].description &&
                                    evnt.date == eventsToCreate[i].date))
                    await _events.InsertOneAsync(eventsToCreate[i]);
            }

            return eventsToCreate;
        }
    }
}

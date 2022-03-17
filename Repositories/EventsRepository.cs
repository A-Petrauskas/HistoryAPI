using MongoDB.Driver;
using Repositories.Entities;
using System.Collections.Generic;
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
            var task = await _events.FindAsync<EventEntity>(historyEvent => historyEvent.Id == id);

            return await task.FirstOrDefaultAsync();
        }

        public async Task<List<EventEntity>> CreateEventsAsync(List<EventEntity> eventsToCreate)
        {
            await _events.InsertManyAsync(eventsToCreate);

            return eventsToCreate;
        }
    }
}

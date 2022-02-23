using MongoDB.Driver;
using Repositories.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repositories
{
    public class EventsRepository : IEventsRepository
    {
        public readonly IMongoCollection<Event> _events;

        public EventsRepository(IHistoryApiDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _events = database.GetCollection<Event>("events");
        }

        public async Task<List<Event>> GetAllAsync()
        {
            var task = await _events.FindAsync<Event>(_ => true);

            return await task.ToListAsync();
        }
            
        public async Task<Event> GetAsync(string id)
        {
            var task = await _events.FindAsync<Event>(historyEvent => historyEvent.Id == id);

            return await task.FirstOrDefaultAsync();
        }
    }
}

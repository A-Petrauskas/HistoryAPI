using MongoDB.Driver;
using Repositories.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repositories
{
    public class EventsRepository : IEventsRepository
    {
        public readonly IMongoCollection<Event> _event;

        public EventsRepository(IHistoryApiDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _event = database.GetCollection<Event>("events");
        }

        public async Task<List<Event>> GetAsync() =>
            await _event.Find(_ => true).ToListAsync();
    }
}

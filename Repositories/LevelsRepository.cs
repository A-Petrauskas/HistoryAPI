using MongoDB.Driver;
using Repositories.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repositories
{
    public class LevelsRepository : ILevelsRepository
    {
        public readonly IMongoCollection<Level> _level;

        public LevelsRepository(IHistoryApiDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _level = database.GetCollection<Level>("levels");
        }

        public async Task<List<Level>> GetAsync() =>
            await _level.Find(_ => true).ToListAsync();
    }
}

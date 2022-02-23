using MongoDB.Driver;
using Repositories.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repositories
{
    public class LevelsRepository : ILevelsRepository
    {
        public readonly IMongoCollection<Level> _levels;

        public LevelsRepository(IHistoryApiDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _levels = database.GetCollection<Level>("levels");
        }

        public async Task<List<Level>> GetAllAsync()
        {
            var task = await _levels.FindAsync<Level>(_ => true);

            return await task.ToListAsync();
        }
            

        public async Task<Level> GetAsync(string id)
        {
            var task = await _levels.FindAsync<Level>(level => level.Id == id);

            return await task.FirstOrDefaultAsync();
        }
    }
}

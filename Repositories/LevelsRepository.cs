using MongoDB.Driver;
using Repositories.Models;

namespace Repositories
{
    public class LevelsRepository : ILevelsRepository
    {
        private readonly IMongoCollection<Level> _level;
    }
}

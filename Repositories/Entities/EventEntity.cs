using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Repositories.Entities
{
    public class EventEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string date { get; set; }

        public string description { get; set; }

        public string imageSrc { get; set; }
    }
}

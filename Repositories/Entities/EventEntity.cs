using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Repositories.Entities
{
    public class EventEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement]
        public string date { get; set; }

        [BsonElement]
        public string description { get; set; }

        [BsonElement]
        public string imageSrc { get; set; }
    }
}

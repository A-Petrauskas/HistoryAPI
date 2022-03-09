using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace Repositories.Entities
{
    public class LevelEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement]
        public string name { get; set; }

        [BsonElement]
        public string description { get; set; }

        [BsonElement]
        public string imageSrc { get; set; }

        [BsonElement]
        public int eventCount { get; set; }

        [BsonElement]
        public int timeConstraint { get; set; }

        [BsonElement]
        public int mistakes { get; set; }

        [BsonElement]
        public int highScore { get; set; }

        [BsonElement]
        public string highScoreUserId { get; set; }

        [BsonElement]
        public List<EventEntity> Events { get; set; }
    }
}

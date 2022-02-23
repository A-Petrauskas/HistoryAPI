using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace Repositories.Entities
{
    public class Level
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement]
        public string levelName { get; set; }

        [BsonElement]
        public int timeConstraint { get; set; }

        [BsonElement]
        public int mistakes { get; set; }

        [BsonElement]
        public int highScore { get; set; }

        [BsonElement]
        public string highScoreUserId { get; set; }

        [BsonElement]
        public List<Event> Events { get; set; }
    }
}

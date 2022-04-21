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
        
        public string name { get; set; }
        
        public string description { get; set; }
        
        public string imageSrc { get; set; }
        
        public int eventCount { get; set; }

        public int timeConstraint { get; set; }
        
        public int mistakes { get; set; }
        
        public int highScore { get; set; }
        
        public string highScoreUserId { get; set; }
        
        public List<EventEntity> events { get; set; }

        public bool fullDates { get; set; }
    }
}

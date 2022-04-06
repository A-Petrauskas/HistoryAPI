using System.Collections.Generic;

namespace Services.Contracts
{
    public class LevelContract
    {
        public string Id { get; set; }

        public string name { get; set; }

        public string description { get; set; }

        public string imageSrc { get; set; }

        public int eventCount { get; set; }

        public int timeConstraint { get; set; }

        public int mistakes { get; set; }

        public int highScore { get; set; }

        public string highScoreUserId { get; set; }

        public List<EventContract> events { get; set; }
    }
}

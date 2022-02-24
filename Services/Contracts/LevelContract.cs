using System.Collections.Generic;

namespace Services.Contracts
{
    public class LevelContract
    {
        public string Id { get; set; }

        public string levelName { get; set; }

        public int timeConstraint { get; set; }

        public int mistakes { get; set; }

        public int highScore { get; set; }

        public string highScoreUserId { get; set; }

        public List<EventContract> Events { get; set; }
    }
}

using System.Collections.Generic;

namespace Services.Contracts
{
    public class GameOverStatsContract
    {
        public List<EventContract> mistakenEvents { get; set; }

        public int mistakes { get; set; }
    }
}

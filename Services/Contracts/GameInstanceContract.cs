using System;
using System.Collections.Generic;

namespace Services.Contracts
{
    public class GameInstanceContract
    {
        public Guid gameId;

        public string levelid;

        public List<EventContract> levelEvents;

        public List<EventContract> usedEvents;

        public int mistakes;

        public List<EventContract> mistakenEvents;
    }
}

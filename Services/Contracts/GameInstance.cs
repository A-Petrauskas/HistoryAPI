using System;
using System.Collections;
using System.Collections.Generic;

namespace Services.Contracts
{
    public class GameInstance
    {
        public Guid gameId;

        public string levelid;

        public List<EventContract> levelEvents;

        public SortedSet<EventContract> usedEvents;

        public int mistakes;

        public List<EventContract> mistakenEvents;
    }
}

using System;
using System.Collections.Generic;

namespace Services.Contracts
{
    public class GameInstanceContract
    {
        public Guid gameId;

        public string levelid;

        public List<EventContract> levelEventsLeft;

        public List<EventContract> usedEvents;

        public int mistakes;

        public List<EventContract> mistakenEvents;

        public GameStateContract lastGameStateSent;

        public EventContract lastEventContractSent;

        public int mistakesAllowed;

        public EnumFirstTwoEvents firstEventsSent = EnumFirstTwoEvents.baseEvent;

        public bool fullDates;
    }
}

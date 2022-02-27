using System;

namespace Services.Contracts
{
    public class GameInstance
    {
        public Guid gameId;

        public string levelid;

        public LevelContract level;
    }
}

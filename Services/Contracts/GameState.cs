﻿namespace Services.Contracts
{
    public class GameState
    {
        public string description { get; set; }

        public string imageSrc { get; set; }

        public int mistakes { get; set; }

        public EnumGameStatus gameStatus { get; set; }
    }
}
namespace Services.Contracts
{
    public class GameStartContract
    {
        public string gameId { get; set; }

        public int timeConstraint { get; set; }

        public int mistakesAllowed { get; set; }
    }
}

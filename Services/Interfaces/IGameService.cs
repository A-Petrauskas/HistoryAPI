using Services.Contracts;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IGameService
    {
        Task<GameStartContract> StartNewGameAsync(string levelId);

        GameStateContract MakeGuessAsync(GameInstanceContract game, int placementIndex);

        GameInstanceContract CheckGameExists(string gameId);

        GameStateContract GenerateNewEvent(GameInstanceContract game, EnumFirstTwoEvents firstTwoEvents);

        GameOverStatsContract GetGameOverStats(GameInstanceContract gameid);
    }
}

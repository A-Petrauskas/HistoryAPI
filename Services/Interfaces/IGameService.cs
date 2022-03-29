using Services.Contracts;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IGameService
    {
        Task<GameStartContract> StartNewGameAsync(string levelId);

        GameState MakeGuessAsync(GameInstanceContract game, int placementIndex);

        GameInstanceContract CheckGameExists(string gameId);

        GameState GenerateNewEvent(GameInstanceContract game, EnumFirstTwoEvents firstTwoEvents);

        GameOverStatsContract GetGameOverStats(GameInstanceContract gameid);
    }
}

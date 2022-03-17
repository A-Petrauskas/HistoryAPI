using Services.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IGameService
    {
        Task<string> StartNewGameAsync(string levelId);

        GameState MakeGuessAsync(GameInstanceContract game, int placementIndex);

        GameInstanceContract CheckGameExists(string gameId);

        GameState GenerateNewEvent(GameInstanceContract game, EnumFirstTwoEvents firstTwoEvents);
    }
}

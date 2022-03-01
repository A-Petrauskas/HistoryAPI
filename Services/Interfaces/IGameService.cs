using Services.Contracts;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IGameService
    {
        Task<string> StartNewGameAsync(string levelId);

        EventGameContract GetNextEvent(GameInstanceContract game);

        GameInstanceContract CheckGameExists(string gameId);
    }
}

using Services.Contracts;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IGameService
    {
        Task<string> StartNewGameAsync(string levelId);

        Task<EventGameContract> GetNextEventAsync(string gameId);
    }
}

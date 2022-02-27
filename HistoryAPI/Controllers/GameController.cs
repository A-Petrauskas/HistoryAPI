using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using Services.Interfaces;
using System.Threading.Tasks;

namespace HistoryAPI.Controllers
{
    [Route("history/game")]
    [Produces("application/json")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IGameService _gameService;

        public GameController(IGameService gameservice)
        {
            _gameService = gameservice;
        }

        [HttpPost("start/{levelid}")]
        public async Task<ActionResult<string>> StartNewGameAsync(string levelid)
        {
            var gameId = await _gameService.StartNewGameAsync(levelid);

            return Ok(gameId);
        }

        [HttpGet("{gameid}/event")]
        public async Task<ActionResult<EventGameContract>> GetNextEventAsync(string gameid)
        {
            var nextEvent = await _gameService.GetNextEventAsync(gameid);

            return Ok(nextEvent);
        }
    }
}

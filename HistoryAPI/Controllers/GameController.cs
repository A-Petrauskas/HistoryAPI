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

            return Ok(gameId); //change into created at??
        }

        [HttpGet("{gameid}/event")] //TODO: Change into frontend cookie for user identification
        public ActionResult<EventGameContract> GetNextEventAsync(string gameid)
        {
            var game = _gameService.CheckGameExists(gameid);

            if (game == null)
            {
                return NotFound();
            }

            var nextEvent = _gameService.GetNextEvent(game);

            if (nextEvent == null)
            {
                // return _gameService.GetGameFinished();
                return BadRequest(); //Testing
            }

            return Ok(nextEvent);
        }
    }
}

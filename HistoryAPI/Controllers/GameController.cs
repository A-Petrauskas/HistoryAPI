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

        [HttpPost]
        public async Task<ActionResult<GameStartContract>> StartNewGameAsync([FromBody] LevelIdContract levelId)
        {
            var gameStartContract = await _gameService.StartNewGameAsync(levelId.levelId);

            return Ok(gameStartContract); // TODO: change into created at
        }


        [HttpPost("{gameid}")] //TODO: Change into frontend cookie for user identification
        public ActionResult<GameState> MakeGuessAsync(string gameid, [FromBody] GuessContract guessContract)
        {
            var game = _gameService.CheckGameExists(gameid);

            if (game == null)
            {
                return NotFound();
            }

            if (game.lastGameStateSent.gameStatus != 0)
            {
                return game.lastGameStateSent;
            }


            if (game.firstEventsSent == EnumFirstTwoEvents.baseEvent)
            {
                var firstGameState = _gameService.GenerateNewEvent(game, EnumFirstTwoEvents.baseEvent);
                game.firstEventsSent = EnumFirstTwoEvents.firstUserEvent;

                return Ok(firstGameState);
            }


            if (game.firstEventsSent == EnumFirstTwoEvents.firstUserEvent)
            {
                var firstGameState = _gameService.GenerateNewEvent(game, EnumFirstTwoEvents.firstUserEvent);
                game.firstEventsSent = EnumFirstTwoEvents.others;

                return Ok(firstGameState);
            }


            var gameState = _gameService.MakeGuessAsync(game, guessContract.placementIndex);

            return Ok(gameState);
        }


        [HttpGet("{gameid}/gameover")]
        public ActionResult<GameOverStatsContract> GetGameOverStats(string gameid)
        {
            var game = _gameService.CheckGameExists(gameid);

            if (game == null)
            {
                return NotFound();
            }

            if (game.lastGameStateSent.gameStatus == EnumGameStatus.stillPlaying)
            {
                return NotFound();
            }

            var gameOverStats = _gameService.GetGameOverStats(game);

            return Ok(gameOverStats);
        }
    }
}

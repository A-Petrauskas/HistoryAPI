using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

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

        /*[HttpPost("new")]
        public async Task<>*/
    }
}

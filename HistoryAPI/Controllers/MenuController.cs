using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HistoryAPI.Controllers
{
    [Route("history")]
    [Produces("application/json")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly IEventsService _eventsService;
        private readonly ILevelsService _levelsService;

        public MenuController(IEventsService eventsService, ILevelsService levelsService)
        {
            _eventsService = eventsService;
            _levelsService = levelsService;
        }

        [HttpGet("events")]
        public async Task<ActionResult<List<EventContract>>> GetEventsAsync() =>
            await _eventsService.GetEventsAsync();


        [HttpGet("events/{id}")]
        public async Task<ActionResult<EventContract>> GetEventAsync(string id) =>
            await _eventsService.GetEventAsync(id);


        [HttpGet("levels")]
        public async Task<ActionResult<List<LevelContract>>> GetLevelsAsync() =>
            await _levelsService.GetLevelsAsync();


        [HttpGet("levels/id/{id}")]
        public async Task<ActionResult<LevelContract>> GetLevelAsync(string id) =>
            await _levelsService.GetLevelAsync(id);

        [HttpGet("levels/name/{name}")]
        public async Task<ActionResult<LevelContract>> GetLevelByNameAsync(string name)
        {
            return await _levelsService.GetLevelByNameAsync(name);
        }

    }
}

using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HistoryAPI.Controllers
{
    [Route("")]
    [Produces("application/json")]
    [ApiController]
    public class ResourceController : ControllerBase
    {
        private readonly IEventsService _eventsService;
        private readonly ILevelsService _levelsService;

        public ResourceController(IEventsService eventsService, ILevelsService levelsService)
        {
            _eventsService = eventsService;
            _levelsService = levelsService;
        }

        [HttpGet("events")]
        public async Task<ActionResult<List<EventContract>>> GetEventsAsync() =>
            Ok(await _eventsService.GetEventsAsync());


        [HttpGet("events/{id}")]
        public async Task<ActionResult<EventContract>> GetEventAsync(string id)
        {
            var eventById = await _eventsService.GetEventAsync(id);

            if (eventById == null)
            {
                return NotFound();
            }

            return Ok(eventById);
        }


        [HttpGet("levels")]
        public async Task<ActionResult<List<LevelContract>>> GetLevelsAsync() =>
            Ok(await _levelsService.GetLevelsAsync());


        [HttpGet("levels/id/{id}")]
        public async Task<ActionResult<LevelContract>> GetLevelAsync(string id)
        {
            var levelById = await _levelsService.GetLevelAsync(id);

            if (levelById == null)
            {
                return NotFound();
            }

            return Ok(levelById);
        }


        [HttpGet("levels/name/{name}")]
        public async Task<ActionResult<LevelContract>> GetLevelByNameAsync(string name)
        {
            var levelByName = await _levelsService.GetLevelByNameAsync(name);

            if (levelByName == null)
            {
                return NotFound();
            }

            return Ok(levelByName);
        }

    }
}

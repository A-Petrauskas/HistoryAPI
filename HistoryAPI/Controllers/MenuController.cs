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
        public async Task<List<EventContract>> GetEventsAsync() =>
            await _eventsService.GetEventsAsync();

        [HttpGet("events/{id}")]
        public async Task<EventContract> GetEventAsync(string id) =>
            await _eventsService.GetEventAsync(id);

        [HttpGet("levels")]
        public async Task<List<LevelContract>> GetLevelsAsync() =>
            await _levelsService.GetLevelsAsync();

        [HttpGet("levels/{id}")]
        public async Task<LevelContract> GetLevelAsync(string id) =>
            await _levelsService.GetLevelAsync(id);
    }
}

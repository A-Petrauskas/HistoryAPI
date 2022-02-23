using Microsoft.AspNetCore.Mvc;
using Repositories.Entities;
using Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HistoryAPI.Controllers
{
    [Route("history")]
    [Produces("application/json")]
    [ApiController]
    public class LevelsController : ControllerBase
    {
        private readonly IEventsService _eventsService;
        private readonly ILevelsService _levelsService;

        public LevelsController(IEventsService eventsService, ILevelsService levelsService)
        {
            _eventsService = eventsService;
            _levelsService = levelsService;
        }


        [HttpGet("events")]
        public async Task<List<Event>> GetEventsAsync() =>
            await _eventsService.GetEventsAsync();

        [HttpGet("events/{id}")]
        public async Task<Event> GetEventAsync(string id) =>
            await _eventsService.GetEventAsync(id);

        [HttpGet("levels")]
        public async Task<List<Level>> GetLevelsAsync() =>
            await _levelsService.GetLevelsAsync();

        [HttpGet("levels/{id}")]
        public async Task<Level> GetLevelAsync(string id) =>
            await _levelsService.GetLevelAsync(id);
    }
}

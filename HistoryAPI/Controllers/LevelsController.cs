using Microsoft.AspNetCore.Mvc;
using Repositories.Entities;
using Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HistoryAPI.Controllers
{
    [Route("HistoryApi")]
    [Produces("application/json")]
    [ApiController]
    public class LevelsController : ControllerBase
    {
        private readonly IEventsService _eventService;
        private readonly ILevelsService _levelsService;

        public LevelsController(IEventsService eventService, ILevelsService levelsService)
        {
            _eventService = eventService;
            _levelsService = levelsService;
        }

        [HttpGet("events")]
        public async Task<List<Event>> GetEventsAsync() =>
            await _eventService.GetAllEventsAsync();

        [HttpGet("levels")]
        public async Task<List<Level>> GetLevelsAsync() =>
            await _levelsService.GetAllEventsAsync();
    }
}

using Microsoft.AspNetCore.Mvc;
using Repositories.Models;
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
        private readonly IEventService _eventService;

        public LevelsController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [HttpGet]
        public async Task<List<Event>> Get() =>
            await _eventService.GetAllEventsAsync();
    }
}

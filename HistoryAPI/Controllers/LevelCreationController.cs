using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using Services.Interfaces;
using System.Threading.Tasks;

namespace HistoryAPI.Controllers
{
    [Route("history/creation")]
    [Produces("application/json")]
    [ApiController]
    public class LevelCreationController : ControllerBase
    {
        private readonly ILevelsService _levelsService;

        public LevelCreationController(ILevelsService levelsService)
        {
            _levelsService = levelsService;
        }

        [HttpPost]
        public async Task<ActionResult<LevelContract>> CreateLevelAsync([FromBody] LevelContract newLevel)
        {
            var newLevelContract = await _levelsService.CreateLevelAsync(newLevel);

            return Ok(newLevelContract); //Created at 
        }

        [HttpPut]
        public async Task<ActionResult<LevelContract>> UpdateLevelAsync([FromBody] LevelContract level)
        {
            var updatedLevelContract = await _levelsService.UpdateLevelAsync(level);

            return Ok(updatedLevelContract); //Created at
        }
    }
}

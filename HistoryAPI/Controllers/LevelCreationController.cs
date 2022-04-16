using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using Services.Interfaces;
using System.Threading.Tasks;

namespace HistoryAPI.Controllers
{
    [Route("creation")]
    [Produces("application/json")]
    [ApiController]
    public class LevelCreationController : ControllerBase
    {
        private readonly ILevelsService _levelsService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public LevelCreationController(ILevelsService levelsService, IWebHostEnvironment webHostEnvironment)
        {
            _levelsService = levelsService;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpPost]
        public async Task<ActionResult<LevelContract>> CreateLevelAsync([FromForm] CreationContract newLevel)
        {
            var newLevelContract = await _levelsService.CreateLevelAsync(newLevel, _webHostEnvironment.ContentRootPath);

            if (newLevelContract == null)
            {
                return Conflict();
            }

            return Ok(newLevelContract);
        }

        [HttpPut]
        public async Task<ActionResult<LevelContract>> UpdateLevelAsync([FromBody] LevelContract level)
        {
            var updatedLevelContract = await _levelsService.UpdateLevelAsync(level);

            return Ok(updatedLevelContract);
        }

        [HttpDelete("{levelid}")]
        public async Task<ActionResult> DeleteLevelAsync(string levelid)
        {
            var level = await _levelsService.GetLevelAsync(levelid);

            if (level == null) return NotFound();

            await _levelsService.RemoveLevelAsync(levelid);

            return Ok();
        }
    }
}

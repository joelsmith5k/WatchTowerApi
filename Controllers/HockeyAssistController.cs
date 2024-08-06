using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using WatchTowerApi.Models;

namespace WatchTowerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HockeyAssistController : ControllerBase
    {
        private readonly WatchTowerContext _context;

        public HockeyAssistController(WatchTowerContext context)
        {
            _context = context;
        }

        // GET: api/HockeyAssists
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HockeyAssist>>> GetHockeyAssists()
        {
            return await _context.HockeyAssist
                                 .ToListAsync();
        }

        // GET: api/HockeyAssists/goalie/{goalieId}
        [HttpGet("goalie/{goalieId}")]
        public async Task<ActionResult<IEnumerable<HockeyAssist>>> GetHockeyAssistsByGoalieId(int goalieId)
        {
            var assists = await _context.HockeyAssist
                                        .Where(ha => ha.GoalieID == goalieId)
                                        .ToListAsync();

            if (assists == null || !assists.Any())
            {
                return NotFound();
            }

            return Ok(assists);
        }
    }
}
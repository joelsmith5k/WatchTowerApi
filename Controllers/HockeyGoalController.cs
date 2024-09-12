using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using WatchTowerApi.Models;

namespace WatchTowerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HockeyGoalController : ControllerBase
    {
        private readonly WatchTowerContext _context;

        public HockeyGoalController(WatchTowerContext context)
        {
            _context = context;
        }

        // GET: api/HockeyGoals
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HockeyGoal>>> GetHockeyGoals()
        {
            return await _context.HockeyGoal
                                 .ToListAsync();
        }


        // GET: api/HockeyGoals/goalie/{goalieId}
        [HttpGet("goalie/{goalieId}")]
        public async Task<ActionResult<IEnumerable<HockeyGoal>>> GetHockeyGoalsByGoalieId(int goalieId)
        {
            var goals = await _context.HockeyGoal
                                       .Where(hg => hg.GoalieID == goalieId)
                                       .ToListAsync();

            if (goals == null || !goals.Any())
            {
                return NotFound();
            }

            return Ok(goals);
        }
    }
}
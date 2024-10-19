using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using WatchTowerApi.Models;
using Microsoft.AspNetCore.Cors;

namespace WatchTowerApi.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("AllowSpecificOrigin")]
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
                                        .Include(ha => ha.HockeyPlayer)
                                        .Include(ha => ha.HockeyTeam)
                                        .Include(ha => ha.HockeyPosition)
                                        .Include(ha => ha.HockeyGoalie)
                                        .ToListAsync();

            if (assists == null || !assists.Any())
            {
                return NotFound();
            }

            return Ok(assists);
        }

        // GET: api/HockeyGoals/goalie/{goalieId}/aggregate
        [HttpGet("goalie/{goalieId}/aggregate")]
        public async Task<ActionResult<IEnumerable<HockeyGoal>>> GetHockeyAssistSummariesByGoalieId(int goalieId)
        {
            var aggregatedGoals = await _context.HockeyAssist
                                                .Where(ha => ha.GoalieID == goalieId)
                                                .Include(ha => ha.HockeyPlayer)
                                                .ThenInclude(hp => hp.DefaultTeam)
                                                .GroupBy(entity => new
                                                {
                                                    entity.PlayerID,
                                                    entity.HockeyPlayer.FirstName,
                                                    entity.HockeyPlayer.LastName,
                                                    entity.HockeyTeam.CityCode
                                                })
                                                .Select(group => new HockeyAssistSummaryGridItem()
                                                {
                                                    PlayerID = group.Key.PlayerID,
                                                    FirstName = group.Key.FirstName,
                                                    LastName = group.Key.LastName,
                                                    CityCode = group.Key.CityCode,

                                                    AssistCount = group.Count()
                                                })
                                                .ToListAsync();

            if (aggregatedGoals == null || !aggregatedGoals.Any())
            {
                return NotFound();
            }

            return Ok(aggregatedGoals);
        }
    }
}
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
                                       .Include(hg => hg.HockeyPlayer)
                                       .Include(hg => hg.PlayerTeam)
                                       .Include(hg => hg.GoalieTeam)
                                       .Include(hg => hg.HockeyPosition)
                                       .Include(hg => hg.HockeyGoalie)
                                       .ToListAsync();

            if (goals == null || !goals.Any())
            {
                return NotFound();
            }

            return Ok(goals);
        }

        // GET: api/HockeyGoals/goalie/{goalieId}/aggregate
        [HttpGet("goalie/{goalieId}/aggregate")]
        public async Task<ActionResult<IEnumerable<HockeyGoal>>> GetHockeyGoalSummariesByGoalieId(int goalieId)
        {
            var aggregatedGoals = await _context.HockeyGoal
                                                .Where(hg => hg.GoalieID == goalieId)
                                                .Include(hg => hg.HockeyPlayer)
                                                .ThenInclude(hp => hp.DefaultTeam)
                                                .GroupBy(entity => new
                                                {
                                                    entity.PlayerID,
                                                    entity.HockeyPlayer.FirstName,
                                                    entity.HockeyPlayer.LastName,
                                                    entity.HockeyPlayer.DefaultTeam.CityCode
                                                })
                                                .Select(group => new HockeyGoalSummaryGridItem()
                                                {
                                                    PlayerID = group.Key.PlayerID,
                                                    FirstName = group.Key.FirstName,
                                                    LastName = group.Key.LastName,
                                                    CityCode = group.Key.CityCode,

                                                    GoalCount = group.Count()
                                                })
                                                .ToListAsync();

            if (aggregatedGoals == null || !aggregatedGoals.Any())
            {
                return NotFound();
            }

            return Ok(aggregatedGoals);
        }

        [HttpGet("goalie/{goalieId}/pieChartData")]
        public async Task<ActionResult<IEnumerable<HockeyGoal>>> GetHockeyGoalPieChartData(int goalieId)
        {
            var pieChartObjects = await _context.HockeyGoal
                                                .Where(hg => hg.GoalieID == goalieId)
                                                .Include(hg => hg.HockeyPlayer)
                                                .ThenInclude(hp => hp.DefaultPosition)
                                                .GroupBy(entity => new
                                                {
                                                    entity.HockeyPlayer.DefaultPosition.HockeyPositionCode
                                                })
                                                .Select(group => new HockeyGoalPieChartItem()
                                                {
                                                    PositionCode = group.Key.HockeyPositionCode,
                                                    GoalCount = group.Count()
                                                })
                                                .ToListAsync();

            if (pieChartObjects == null || !pieChartObjects.Any())
            {
                return NotFound();
            }

            return Ok(pieChartObjects);
        }


    }
}
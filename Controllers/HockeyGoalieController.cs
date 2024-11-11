using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WatchTowerApi.Models;
using Microsoft.AspNetCore.Cors;

namespace WatchTowerApi.Controllers
{

    [Route("api/[controller]")]
    [EnableCors("AllowSpecificOrigin")]
    [ApiController]
    public class HockeyGoalieController : ControllerBase
    {
        private readonly WatchTowerContext _context;

        public HockeyGoalieController(WatchTowerContext context)
        {
            _context = context;
        }

        // GET: api/HockeyGoalie
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HockeyGoalie>>> GetHockeyGoalies()
        {
            return await _context.HockeyGoalie.Include(b => b.HockeyLeague).ToListAsync();
        }

        // GET: api/HockeyGoalie/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HockeyGoalie>> GetHockeyGoalie(int id)
        {
            var hockeyGoalie = await _context.HockeyGoalie.FindAsync(id);

            if (hockeyGoalie == null)
            {
                return NotFound();
            }

            return hockeyGoalie;
        }

        // GET: api/HockeyGoalie/5
        [HttpGet("league/{id}")]
        public async Task<ActionResult<IEnumerable<HockeyGoalie>>> GetHockeyGoaliesByLeague(int id)
        {
            var hockeyGoalies = await _context.HockeyGoalie.Where(g => g.LeagueID == id && g.HockeyGoals.Any())
                                                            .Include(g => g.HockeyLeague)
                                                            .Include(g => g.HockeyTeam)
                                                            .OrderBy(g => g.LastName).ToListAsync();

            if (hockeyGoalies == null)
            {
                return NotFound();
            }

            return hockeyGoalies;
        }

        // POST: api/HockeyGoalie
        [HttpPost]
        public async Task<ActionResult<HockeyGoalie>> PostHockeyGoalie(HockeyGoalie hockeyGoalie)
        {
            _context.HockeyGoalie.Add(hockeyGoalie);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetHockeyGoalie), new { id = hockeyGoalie.GoalieID }, hockeyGoalie);
        }

        // PUT: api/HockeyGoalie/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHockeyGoalie(int id, HockeyGoalie hockeyGoalie)
        {
            if (id != hockeyGoalie.GoalieID)
            {
                return BadRequest();
            }

            _context.Entry(hockeyGoalie).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HockeyGoalieExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/HockeyGoalie/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHockeyGoalie(int id)
        {
            var hockeyGoalie = await _context.HockeyGoalie.FindAsync(id);
            if (hockeyGoalie == null)
            {
                return NotFound();
            }

            _context.HockeyGoalie.Remove(hockeyGoalie);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HockeyGoalieExists(int id)
        {
            return _context.HockeyGoalie.Any(e => e.GoalieID == id);
        }

    }


}
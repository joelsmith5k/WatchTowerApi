using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WatchTowerApi.Models;

namespace WatchTowerApi.Controllers

{
    [Route("api/[controller]")]
    [ApiController]
    public class HockeyLeagueController : ControllerBase
    {
        private readonly WatchTowerContext _context;

        public HockeyLeagueController(WatchTowerContext context)
        {
            _context = context;
        }

        // GET: api/HockeyLeague
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HockeyLeague>>> GetHockeyLeagues()
        {
            return await _context.HockeyLeague.ToListAsync();
        }

        // GET: api/HockeyLeague/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HockeyLeague>> GetHockeyLeague(int id)
        {
            var hockeyLeague = await _context.HockeyLeague.FindAsync(id);

            if (hockeyLeague == null)
            {
                return NotFound();
            }

            return hockeyLeague;
        }

        // POST: api/HockeyLeague
        [HttpPost]
        public async Task<ActionResult<HockeyLeague>> PostHockeyLeague(HockeyLeague hockeyLeague)
        {
            _context.HockeyLeague.Add(hockeyLeague);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetHockeyLeague), new { id = hockeyLeague.LeagueID }, hockeyLeague);
        }

        // PUT: api/HockeyLeague/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHockeyLeague(int id, HockeyLeague hockeyLeague)
        {
            if (id != hockeyLeague.LeagueID)
            {
                return BadRequest();
            }

            _context.Entry(hockeyLeague).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HockeyLeagueExists(id))
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

        // DELETE: api/HockeyLeague/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHockeyLeague(int id)
        {
            var hockeyLeague = await _context.HockeyLeague.FindAsync(id);
            if (hockeyLeague == null)
            {
                return NotFound();
            }

            _context.HockeyLeague.Remove(hockeyLeague);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HockeyLeagueExists(int id)
        {
            return _context.HockeyLeague.Any(e => e.LeagueID == id);
        }
    }
}
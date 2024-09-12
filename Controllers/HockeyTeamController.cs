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
    public class HockeyTeamController : ControllerBase
    {
        private readonly WatchTowerContext _context;

        public HockeyTeamController(WatchTowerContext context)
        {
            _context = context;
        }

        // GET: api/HockeyTeam
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HockeyTeam>>> GetHockeyTeams()
        {
            return await _context.HockeyTeam.ToListAsync();
        }

        // GET: api/HockeyTeam/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HockeyTeam>> GetHockeyTeam(int id)
        {
            var hockeyTeam = await _context.HockeyTeam.FindAsync(id);

            if (hockeyTeam == null)
            {
                return NotFound();
            }

            return hockeyTeam;
        }

        // POST: api/HockeyTeam
        [HttpPost]
        public async Task<ActionResult<HockeyTeam>> PostHockeyTeam(HockeyTeam hockeyTeam)
        {
            _context.HockeyTeam.Add(hockeyTeam);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetHockeyTeam), new { id = hockeyTeam.TeamID }, hockeyTeam);
        }

        // PUT: api/HockeyTeam/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHockeyTeam(int id, HockeyTeam hockeyTeam)
        {
            if (id != hockeyTeam.TeamID)
            {
                return BadRequest();
            }

            _context.Entry(hockeyTeam).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HockeyTeamExists(id))
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

        // DELETE: api/HockeyTeam/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHockeyTeam(int id)
        {
            var hockeyTeam = await _context.HockeyTeam.FindAsync(id);
            if (hockeyTeam == null)
            {
                return NotFound();
            }

            _context.HockeyTeam.Remove(hockeyTeam);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HockeyTeamExists(int id)
        {
            return _context.HockeyTeam.Any(e => e.TeamID == id);
        }
    }
}
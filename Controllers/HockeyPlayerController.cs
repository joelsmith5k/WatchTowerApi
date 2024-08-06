using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WatchTowerApi.Models;

namespace WatchTowerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HockeyPlayerController : ControllerBase
    {
        private readonly WatchTowerContext _context;

        public HockeyPlayerController(WatchTowerContext context)
        {
            _context = context;
        }

        // GET: api/HockeyPlayer
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HockeyPlayer>>> GetHockeyPlayers()
        {
            return await _context.HockeyPlayer.ToListAsync();
        }

        // GET: api/HockeyPlayer/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HockeyPlayer>> GetHockeyPlayer(int id)
        {
            var hockeyPlayer = await _context.HockeyPlayer.FindAsync(id);

            if (hockeyPlayer == null)
            {
                return NotFound();
            }

            return hockeyPlayer;
        }


        // GET: api/HockeyPlayer/lastname/A
        [HttpGet("lastname/{letter}")]
        public async Task<ActionResult<IEnumerable<HockeyPlayer>>> GetPlayersByLastNameLetter(char letter)
        {
            if (char.IsWhiteSpace(letter) || !char.IsLetter(letter) || letter.ToString().Length != 1)
            {
                return BadRequest("Invalid letter provided.");
            }

            var hockeyPlayers = await _context.HockeyPlayer
                .Where(p => p.LastName.StartsWith(letter.ToString()))
                .ToListAsync();

            if (hockeyPlayers == null || !hockeyPlayers.Any())
            {
                return NotFound();
            }

            return hockeyPlayers;
        }
    }
}
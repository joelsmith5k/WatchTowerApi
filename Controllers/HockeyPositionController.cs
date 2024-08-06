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
    public class HockeyPositionController : ControllerBase
    {
        private readonly WatchTowerContext _context;

        public HockeyPositionController(WatchTowerContext context)
        {
            _context = context;
        }

        // GET: api/HockeyPosition
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HockeyPosition>>> GetHockeyPositions()
        {
            return await _context.HockeyPosition.ToListAsync();
        }

        // GET: api/HockeyPosition/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HockeyPosition>> GetHockeyPosition(int id)
        {
            var hockeyPosition = await _context.HockeyPosition.FindAsync(id);

            if (hockeyPosition == null)
            {
                return NotFound();
            }

            return hockeyPosition;
        }
    }
}
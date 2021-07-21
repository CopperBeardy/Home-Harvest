using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HomeHarvest.Server.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using HomeHarvest.Server.Entities;

namespace HomeHarvest.Server.Controllers
{
	[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SowController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
		private readonly ILogger<SowController> _logger;

		public SowController(ApplicationDbContext context, ILogger<SowController> logger)
        {
            _context = context;
			_logger = logger;
		}

        // GET: api/Sow
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sow>>> GetSows()=>
            await _context.Sows.ToListAsync();
        

        // GET: api/Sow/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Sow>> GetSow(int id)
        {
            var sow = await _context.Sows
                .Include(p => p.Plant)
                .FirstOrDefaultAsync(x => x.Id.Equals(id));

            if (sow == null)
            {
                return NotFound();
            }

            return sow;
        }

        // PUT: api/Sow/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSow(int id, Sow sow)
        {
            if (id != sow.Id)
            {
                return BadRequest();
            }
            _context.Entry(sow).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Sow object with Id {sow.Id} has been modified in the Db ");

            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!SowExists(id))
                {
                    _logger.LogInformation($"Sow object with Id {sow.Id} was not found in Db ");
                    return NotFound();
                }
                else
                {
                    _logger.LogError($"Sow object with Id {sow.Id} has been modified in the Db ",ex);
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Sow
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Sow>> PostSow(Sow sow)
        {
            _context.Sows.Add(sow);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"New Sow Object with Id {sow.Id} has been added to the Db ");
            return CreatedAtAction("GetSow", new { id = sow.Id }, sow);
        }

        // DELETE: api/Sow/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSow(int id)
        {
            var sow = await _context.Sows.FindAsync(id);
            if (sow == null)
            {              
                return NotFound();
            }

            _context.Sows.Remove(sow);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"Sow item with Id {id} has been removed Db ");

            return NoContent();
        }

        private bool SowExists(int id) => _context.Sows.Any(e => e.Id == id);
        
    }
}

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
    public class CropController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
		private readonly ILogger<CropController> _logger;

		public CropController(ApplicationDbContext context, ILogger<CropController> logger)
        {
            _context = context;
			_logger = logger;
		}

        // GET: api/Crop
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Crop>>> GetCrops() => 
            await _context.Crops.ToListAsync();
        

        // GET: api/Crop/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Crop>> GetCrop(int id)
        {
            if (CropExists(id))
            {               
                return await _context.Crops
                    .Include(s => s.Sowed)
                    .FirstOrDefaultAsync(x => x.Id.Equals(id));
            } else
            {
                return NotFound();
            }
        }

        //// PUT: api/Crop/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutCrop(int id, Crop crop)
        //{
        //    if (id != crop.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(crop).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!CropExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        // POST: api/Crop
      [HttpPost]
        public async Task<ActionResult<Crop>> PostCrop(Crop crop)
        {
            _context.Crops.Add(crop);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"New Crop item with Id {crop.Id} has been added to the Db ");
            return CreatedAtAction("GetCrop", new { id = crop.Id }, crop);
        }

        // DELETE: api/Crop/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCrop(int id)
        {
            var crop = await _context.Crops.FindAsync(id);
			if (crop == null)
            {
                return NotFound();
            }
            
            _context.Crops.Remove(crop);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"Crop with Id: {id} has been remove from Db");

            return NoContent();
        }
        private bool CropExists(int id) => _context.Crops.Any(e => e.Id == id);
	}
}

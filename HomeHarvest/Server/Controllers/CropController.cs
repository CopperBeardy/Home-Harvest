using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HomeHarvest.Server.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using HomeHarvest.Server.Entities;
using HomeHarvest.Shared.Dtos;
using AutoMapper;
using HomeHarvest.Server.Services;

namespace HomeHarvest.Server.Controllers
{
	[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CropController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
		private readonly ILogger<CropController> _logger;
		private readonly IMapper _mapper;
		private readonly IBlobService _blobService;

		public CropController(ApplicationDbContext context, ILogger<CropController> logger, IMapper mapper,IBlobService blobService)
        {
            _context = context;
			_logger = logger;
			_mapper = mapper;
			_blobService = blobService;
		}

        // GET: api/Crop
        [HttpGet]
        public async Task<ActionResult<List<CropDto>>> GetCrops() =>        
             _mapper.Map<List<CropDto>>(   await _context.Crops.ToListAsync());
        

        // GET: api/Crop/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CropDto>> GetCrop(int id)
        {
            if (CropExists(id))
            {               
                var crop =  await _context.Crops
                    .Include(s => s.Sowed)
                    .FirstOrDefaultAsync(x => x.Id.Equals(id));
                return _mapper.Map<CropDto>(crop);
            } else
            {
                return NotFound();
            }
        }

		//// PUT: api/Crop/5
		//// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPut("{id}")]
		public async Task<IActionResult> PutCrop(int id, CropDto cropDto)
		{
            var crop = _mapper.Map<Crop>(cropDto);  
			if (id != crop.Id)
			{
				return BadRequest();
			}
            
			_context.Entry(crop).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!CropExists(id))
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

		// POST: api/Crop
		[HttpPost]
        public async Task<bool> PostCrop(CreateCropDto cropDto)
        {
            await _blobService.Upload(cropDto.Image,cropDto.PlotImage);
            var crop = _mapper.Map<Crop>(cropDto);
			try
			{
                crop.Year = DateTime.Now.Year;
                _context.Crops.Add(crop);
                await _context.SaveChangesAsync();
                _logger.LogInformation($"New Crop item with Id {crop.Id} has been added to the Db ");
                return true;
			}
			catch (Exception ex)
			{
                _logger.LogError($"Exception occured try to add {crop.Location},{crop.Year} to dataase: {ex}");
		         }
            return false;
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

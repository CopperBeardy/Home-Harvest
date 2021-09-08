using AutoMapper;
using HomeHarvest.Server.Data;
using HomeHarvest.Server.Entities;
using HomeHarvest.Shared.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HomeHarvest.Server.Controllers
{
	[Authorize]
	[Route("api/[controller]")]
	[ApiController]
	public class PlantController : ControllerBase
	{
		private readonly ApplicationDbContext _context;
		private readonly ILogger<SownController> _logger;
		private readonly IMapper _mapper;
		public PlantController(ApplicationDbContext context, ILogger<SownController> logger, IMapper mapper)
		{
			_context = context;
			_logger = logger;
			_mapper = mapper;
		}

		[HttpGet]
		public async Task<ActionResult<List<PlantDto>>> GetPlants() => 
			_mapper.Map<List<PlantDto>>(await _context.Plants.ToListAsync());


		[HttpGet("{id}")]
		public async Task<ActionResult<PlantDto>> GetPlant(int id)
		{		
			if (PlantExists(id))
			{
				var plant = await _context.Plants				
				.FirstOrDefaultAsync(x => x.Id.Equals(id));
				return _mapper.Map<PlantDto>(plant);
			}
            else
            {	
				_logger.LogError($"plant with {id} could not be found");
				return NotFound();
            }
		}

		[HttpPut]
		public async Task<IActionResult> PutPlant (PlantDto plant)
		{
			var entity = _mapper.Map<Plant>(plant);
			_context.Update(entity);
	
				try
				{
					await _context.SaveChangesAsync();
					_logger.LogInformation($"Plant object with Id {plant.Id} has been modified in the Db ");
				}
				catch (Exception ex)
				{
					
					_logger.LogError($"Plant object with Id {plant.Id} has encountered a update concurrencyException", ex);
				}
		
			return NoContent();
		}

	
		[HttpPost]
		public async Task<ActionResult> PostPlant(PlantDto plant)
        {
            try
            {
                var entity = _mapper.Map<Plant>(plant);
                _context.Plants.Add(entity);
                await _context.SaveChangesAsync();
                return Ok();
            }
			catch (Exception ex)
			{
				_logger.LogError($"Exception occured trying to insert {plant.Name} to dataase: {ex}");
				throw new Exception("Exception occured ", ex);
			}
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeletePlant(int id)
		{
			var plant = await _context.Plants.FindAsync(id);
			if (plant == null)
			{
				_logger.LogError($"Plant with {id} could not be found");
				return NotFound();
			}

			_context.Plants.Remove(plant);
			await _context.SaveChangesAsync();
			_logger.LogInformation($"Plant item with Id {id} has been removed Db ");
			return Ok();
		}

		private bool PlantExists(int id) => _context.Plants.Any(e => e.Id == id);

	}
}

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

		// GET: api/Plant
		[HttpGet]
		public async Task<ActionResult<List<PlantDto>>> GetPlants() => 
			_mapper.Map<List<PlantDto>>(await _context.Plants.ToListAsync());

		//// GET: api/Plant/5
		[HttpGet("{id}")]
		public async Task<ActionResult<PlantDto>> GetPlant(int id)
		{
			var plant = await _context.Plants				
				.FirstOrDefaultAsync(x => x.Id.Equals(id));

			if (plant == null)
			{
				return NotFound();
			}

			return _mapper.Map<PlantDto>(plant);
		}

		// PUT: api/Plant/5
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPut("{id}")]
		public async Task<IActionResult> PutPlant (int id, PlantDto plant)
		{
			if (id != plant.Id)
			{
				return BadRequest();
			}
			var entity = _mapper.Map<Plant>(plant);
			_context.Plants.Update(entity);

			try
			{
				await _context.SaveChangesAsync();
				_logger.LogInformation($"Plant object with Id {plant.Id} has been modified in the Db ");

			}
			catch (DbUpdateConcurrencyException ex)
			{
				if (!PlantExists(id))
				{
					_logger.LogInformation($"Plant object with Id {plant.Id} was not found in Db ");
					return NotFound();
				}
				else
				{
					_logger.LogError($"Plant object with Id {plant.Id} has been modified in the Db ", ex);
					throw;
				}
			}

			return NoContent();
		}

		// POST: api/Plant
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPost]
		public async Task<ActionResult> PostPlant(CreatePlantDto plant)
		{
			var entity = _mapper.Map<Plant>(plant);
			_context.Plants.Add(entity);
			await _context.SaveChangesAsync();
			return Ok();
		}

		// DELETE: api/Plant/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeletePlant(int id)
		{
			var plant = await _context.Plants.FindAsync(id);
			if (plant == null)
			{
				return NotFound();
			}

			_context.Plants.Remove(plant);
			await _context.SaveChangesAsync();
			_logger.LogInformation($"Plant item with Id {id} has been removed Db ");

			return NoContent();
		}

		private bool PlantExists(int id) => _context.Plants.Any(e => e.Id == id);

	}
}

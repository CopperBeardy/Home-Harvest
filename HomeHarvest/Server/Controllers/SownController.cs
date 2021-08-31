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
	public class SownController : ControllerBase
	{
		private readonly ApplicationDbContext _context;
		private readonly ILogger<SownController> _logger;
		private readonly IMapper _mapper;
		public SownController(ApplicationDbContext context, ILogger<SownController> logger, IMapper mapper)
		{
			_context = context;
			_logger = logger;
			_mapper = mapper;
		}

		// GET: api/Sow
		[HttpGet]
		public async Task<ActionResult<List<SownDto>>> GetSown() =>
			_mapper.Map<List<SownDto>>(await _context.Sowns.ToListAsync());

		//// GET: api/Sow/5
		[HttpGet("{id}")]
		public async Task<ActionResult<SownDto>> GetSow(int id)
		{
			var sow = await _context.Sowns
				.Include(p => p.Plant)
				.FirstOrDefaultAsync(x => x.Id.Equals(id));

			if (sow == null)
			{
				return NotFound();
			}

			return _mapper.Map<SownDto>(sow);
		}

		// PUT: api/Sow/5
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPut("{id}")]
		public async Task<IActionResult> PutSow(int id, SownDto sow)
		{
			bool savefailed;
			var entity = _mapper.Map<Sown>(sow);
			_context.Update(entity);
			do
			{
				savefailed = false;
				try
				{				
					await _context.SaveChangesAsync();
					_logger.LogInformation($"Sow object with Id {sow.Id} has been modified in the Db ");
				}
				catch (DbUpdateConcurrencyException ex)
				{
					savefailed = true;
					var entry = ex.Entries.Single();
					entry.OriginalValues.SetValues(entry.GetDatabaseValues());
				
				}
			} while (savefailed);
			return NoContent();
		}

		// POST: api/Sow
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPost]
		public async Task<ActionResult> PostSow(CreateSownDto sow)
		{
			var entity = _mapper.Map<Sown>(sow);
			_context.Sowns.Add(entity);
			await _context.SaveChangesAsync();
			return Ok();
		}

		// DELETE: api/Sow/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteSow(int id)
		{
			var sow = await _context.Sowns.FindAsync(id);
			if (sow == null)
			{
				return NotFound();
			}

			_context.Sowns.Remove(sow);
			await _context.SaveChangesAsync();
			_logger.LogInformation($"Sow item with Id {id} has been removed Db ");

			return NoContent();
		}

		private bool SowExists(int id) => _context.Sowns.Any(e => e.Id == id);

	}
}

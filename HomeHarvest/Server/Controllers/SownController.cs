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

		[HttpGet]
		public async Task<ActionResult<List<SownDto>>> GetSown() =>
			_mapper.Map<List<SownDto>>(await _context.Sowns.AsNoTracking().ToListAsync());
			
		[HttpGet("{id}")]
		public async Task<ActionResult<SownDto>> GetSow(int id)
		{
			if (SowExists(id))
			{
				var sow = await _context.Sowns
					  .AsNoTracking()
				.Include(p => p.Plant)
				.FirstOrDefaultAsync(x => x.Id.Equals(id)); 
				return _mapper.Map<SownDto>(sow);
			}
			return NotFound();		
		}

	[HttpPut]
		public async Task<IActionResult> PutSow(SownDto sow)
		{
					var entity = _mapper.Map<Sown>(sow);
			_context.Update(entity);
                try
                {
                    await _context.SaveChangesAsync();
                    _logger.LogInformation($"Sow object with Id {sow.Id} has been modified in the Db ");
                }
                catch (Exception ex)
                {                
					_logger.LogError($"Sow object with Id {sow.Id} has encountered a update concurrencyException", ex);
				 }          
            return NoContent();
		}

		[HttpPost]
		public async Task<ActionResult> PostSow(SownDto sow)
        {
            try
            {
                var entity = _mapper.Map<Sown>(sow);
                _context.Sowns.Add(entity);
                await _context.SaveChangesAsync();
				_logger.LogInformation($"New Sown item with Id {sow.Id} has been added to the Db ");

				return Ok();
            }
            catch (Exception ex)
            {
				_logger.LogError($"Exception occured try to insert {sow.Plant},{sow.PlantedOn} to dataase: {ex}");
				throw new Exception("Exception occured ", ex);
			}
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteSow(int id)
		{
			var sow = await _context.Sowns.FindAsync(id);
			if (sow == null)
			{
				_logger.LogError($"Sown with {id} could not be found");
				return NotFound();
			}

			_context.Sowns.Remove(sow);
			await _context.SaveChangesAsync();
			_logger.LogInformation($"Sow item with Id {id} has been removed Db ");
			return Ok();	
		}

		private bool SowExists(int id) => _context.Sowns.Any(e => e.Id == id);
	}
}

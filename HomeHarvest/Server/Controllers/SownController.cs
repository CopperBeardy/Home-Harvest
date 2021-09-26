using HomeHarvest.Server.Data;
using HomeHarvest.Shared.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HomeHarvest.Server.Controllers
{
	[Authorize]
	[Route("api/[controller]")]
	[ApiController]
	public class SownController : BaseController<Sown>
	{
		public SownController(ApplicationDbContext context, ILogger<SownController> logger)
			: base(context, logger)
		{
		}

		[HttpGet("{id}")]
		public override async Task<ActionResult<Sown>> Get(int id)
		{
			if (EntityExists(id))
			{
				var sow = await _context.Sowns
					  .AsNoTracking()
				.Include(p => p.Plant)
				.FirstOrDefaultAsync(x => x.Id.Equals(id));
				return Ok(sow);
			}
			return NotFound();
		}





	}
}

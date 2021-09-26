
using HomeHarvest.Server.Data;
using HomeHarvest.Server.Services;
using HomeHarvest.Shared.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace HomeHarvest.Server.Controllers
{
	[Authorize]
	[Route("api/[controller]")]
	[ApiController]
	public class CropController : BaseController<Crop>
	{
		private readonly IBlobService _blobService;

		public CropController(ApplicationDbContext context, ILogger<CropController> logger, IBlobService blobService)
			: base(context, logger)
		{
			_blobService = blobService;
		}

		[HttpGet("{id}")]
		public override async Task<ActionResult<Crop>> Get(int id)
		{
			if (EntityExists(id))
			{
				var crop = await _context.Crops
					  .AsNoTracking()
					  .Include(s => s.Sowed.Where(x =>x.CropId == id))
					  .ThenInclude(p => p.Plant)
					  .FirstOrDefaultAsync(x => x.Id.Equals(id));
				return Ok(crop);
			}
			return NotFound();
		}

		[HttpPost]
		public override async Task<ActionResult> Post(Crop crop)
		{
			try
			{
				await _blobService.Upload(crop.Image, crop.PlotImage);	
				crop.Year = DateTime.Now.Year;
				_context.Add(crop);
				await _context.SaveChangesAsync();
				_logger.LogInformation($"New {crop.GetType()} with Id {crop.Id} has been added to the Db ");

				return Ok();
			}
			catch (Exception ex)
			{
				_logger.LogError($"Exception occured try to add {crop} to dataase: {ex}");
				return BadRequest();
			}
		}
	}
}

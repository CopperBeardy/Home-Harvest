using AutoMapper;
using HomeHarvest.Server.Data;
using HomeHarvest.Server.Entities;
using HomeHarvest.Server.Services;
using HomeHarvest.Shared.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HomeHarvest.Server.Controllers
{
	//[Authorize]
	[Route("api/[controller]")]
	[ApiController]
	public class CropController : ControllerBase
	{
		private readonly ApplicationDbContext _context;
		private readonly ILogger<CropController> _logger;
		private readonly IMapper _mapper;
		private readonly IBlobService _blobService;

		public CropController(ApplicationDbContext context, ILogger<CropController> logger, IMapper mapper, IBlobService blobService)
		{
			_context = context;
			_logger = logger;
			_mapper = mapper;
			_blobService = blobService;
		}
	
		[HttpGet]
		public async Task<ActionResult<List<CropDto>>> GetCrops() =>
			 _mapper.Map<List<CropDto>>(await _context.Crops.AsNoTracking().ToListAsync());
				
		[HttpGet("{id}")]
		public async Task<ActionResult<CropDto>> GetCrop(int id)
		{
			if (CropExists(id))
			{
				var crop = await _context.Crops
					  .AsNoTracking()
					.Include(s => s.Sowed)
					.ThenInclude(p => p.Plant)
					.FirstOrDefaultAsync(x => x.Id.Equals(id));
				return _mapper.Map<CropDto>(crop);
			}
			else
			{
				_logger.LogError($"Crop with {id} could not be found");
				return NotFound();
			}
		}
			
		[HttpPost]
		public async Task<IActionResult> PostCrop(CropDto cropDto)
		{
			try
			{
				await _blobService.Upload(cropDto.Image, cropDto.PlotImage);
				var crop = _mapper.Map<Crop>(cropDto);
				crop.Year = DateTime.Now.Year;
				_context.Crops.Add(crop);
				await _context.SaveChangesAsync();
				_logger.LogInformation($"New Crop item with Id {crop.Id} has been added to the Db ");
				return Ok();
			}
			catch (Exception ex)
			{
				_logger.LogError($"Exception occured try to add {cropDto.Location},{cropDto.Year} to dataase: {ex}");
				throw new Exception("Exception occured ", ex);
			}
		}
				
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteCrop(int id)
		{
			var crop = await _context.Crops.FindAsync(id);
			if (crop == null)
			{
				_logger.LogError($"Crop with {id} could not be found");
				return NotFound();
			}			
			
			var success = await _blobService.Delete(crop.PlotImage);
			if (success)
			{	
				_context.Crops.Remove(crop);
				await _context.SaveChangesAsync();
				_logger.LogInformation($"Crop with Id: {id} has been remove from Db");
				return Ok();
			}		
			return NoContent();
		}
		private bool CropExists(int id) =>	_context.Crops.Any(e => e.Id == id);
	}
}

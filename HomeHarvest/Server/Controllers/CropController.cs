//using AutoMapper;
//using HomeHarvest.Server.Data;
//using HomeHarvest.Server.Entities;
//using HomeHarvest.Server.Services;
//using HomeHarvest.Shared.Dtos;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using System.Threading.Tasks;

//namespace HomeHarvest.Server.Controllers
//{
//    [Authorize]
//    [Route("api/[controller]")]
//    [ApiController]
//    public class CropController : BaseController<Crop, CropDto>
//    {
//        private readonly IBlobService _blobService;

//        public CropController(ApplicationDbContext context, ILogger<CropController> logger, IMapper mapper, IBlobService blobService)
//            : base(context, logger, mapper)
//        {
//            _blobService = blobService;
//        }

//        [HttpGet("{id}")]
//        public override async Task<ActionResult<CropDto>> Get(int id)
//        {
//            if (EntityExists(id))
//            {
//                var crop = await _context.Crops
//                      .AsNoTracking()
//                      .Include(s => s.Sowed)
//                      .ThenInclude(p => p.Plant)
//                      .FirstOrDefaultAsync(x => x.Id.Equals(id));
//                var mappedModel = _mapper.Map<CropDto>(crop);
//                return Ok(mappedModel);
//            }
//            return NotFound();
//        }

//        [HttpPost]
//        public override async Task<ActionResult> Post(CropDto cropDto)
//        {
//            try
//            {
//                await _blobService.Upload(cropDto.Image, cropDto.PlotImage);
//                var crop = _mapper.Map<Crop>(cropDto);
//                crop.Year = DateTime.Now.Year;
//                _context.Add(crop);
//                await _context.SaveChangesAsync();
//                _logger.LogInformation($"New {cropDto.GetType()} with Id {crop.Id} has been added to the Db ");

//                return Ok();
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError($"Exception occured try to add {cropDto} to dataase: {ex}");
//                return StatusCode(500);
//            }
//        }
//    }
//}

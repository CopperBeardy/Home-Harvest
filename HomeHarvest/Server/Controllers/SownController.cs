//using AutoMapper;
//using HomeHarvest.Server.Data;
//using HomeHarvest.Server.Entities;
//using HomeHarvest.Shared.Dtos;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;

//namespace HomeHarvest.Server.Controllers
//{
//    [Authorize]
//    [Route("api/[controller]")]
//    [ApiController]
//    public class SownController : BaseController<Sown, SownDto>
//    {
//        public SownController(ApplicationDbContext context, ILogger<SownController> logger, IMapper mapper)
//            : base(context, logger, mapper)
//        {
//        }

//        [HttpGet("{id}")]
//        public override async Task<ActionResult<SownDto>> Get(int id)
//        {
//            if (EntityExists(id))
//            {
//                var sow = await _context.Sowns
//                      .AsNoTracking()
//                .Include(p => p.Plant)
//                .FirstOrDefaultAsync(x => x.Id.Equals(id));
//                return _mapper.Map<SownDto>(sow);
//            }
//            return NotFound();
//        }





//    }
//}

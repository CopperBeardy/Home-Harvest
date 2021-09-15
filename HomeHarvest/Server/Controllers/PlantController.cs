using AutoMapper;
using HomeHarvest.Server.Data;
using HomeHarvest.Server.Entities;
using HomeHarvest.Shared.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HomeHarvest.Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PlantController : BaseController<Plant, PlantDto>
    {
        public PlantController(ApplicationDbContext context, ILogger<SownController> logger, IMapper mapper)
            : base(context, logger, mapper)
        {
        }
    }
}

using AutoMapper;
using HomeHarvest.Client.Services;
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
        ILogger<PlantController> logger;
        IMapper mapper;
        
        public PlantController(IRepository<Plant> repo, ILogger<PlantController> logger, IMapper mapper)
            : base(repo)
        {
            this.logger = logger;
            this.mapper = mapper;
        }
    }
}

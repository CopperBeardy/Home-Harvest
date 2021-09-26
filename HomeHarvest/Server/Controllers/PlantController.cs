using HomeHarvest.Server.Data;
using HomeHarvest.Shared.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HomeHarvest.Server.Controllers
{
	[Authorize]
	[Route("api/[controller]")]
	[ApiController]
	public class PlantController : BaseController<Plant>
	{

		public PlantController(ApplicationDbContext context, ILogger<PlantController> logger)
			: base(context, logger)
		{
		}
	}
}

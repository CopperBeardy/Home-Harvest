using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;
using HomeHarvest.Server.Controllers;
using HomeHarvest.Shared.Dtos;
using HomeHarvest.Server.Entities;
using Microsoft.Extensions.Logging;
using AutoMapper;
using HomeHarvest.Server.Data;

namespace HomeHarvestTests.Server
{
	public class PlantControllerTests
	{
		PlantController _plantController;
		public PlantControllerTests()
		{
			var logger = new Mock<ILogger<PlantController>>();
			var mapper = new Mock<IMapper>();
			var context = new Mock<ApplicationDbContext>();
			_plantController = new PlantController<Plant, PlantDto>(context.Object,logger.Object,mapper.Object);
		}

		[Fact]
		public async Task ReturnAllItems()
		{

		}
	}
}

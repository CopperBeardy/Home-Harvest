using FluentAssertions;
using HomeHarvest.Server.Controllers;
using HomeHarvest.Shared.Entities;
using HomeHarvest.Shared.Enums;
using HomeHarvestTests.TestHelpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace HomeHarvestTests.Server
{
	[ExcludeFromCodeCoverage]
	public class PlantControllerTests
	{

		[Fact]
		public async Task GetAll_reutrns_list_plants()
		{
			//Arrange
			var loggerMock = new Mock<ILogger<PlantController>>();
			var context = ContextDouble.CreateDbContext();	
			var sut = new PlantController(context,loggerMock.Object);
			var expected = Plants();

			// Act
			var result = await sut.GetAll();

			// Assert
			result.Should().BeOfType<ActionResult<IEnumerable<Plant>>>();
			result.Result.As<OkObjectResult>().Value.Should().BeEquivalentTo(expected);

		}

		[Theory]
		[InlineData(1)]
		[InlineData(2)]
		public async Task Get_with_valid_id_returns_cropdto(int id)
		{
			//Arrange
			var loggerMock = new Mock<ILogger<PlantController>>();
			var context = ContextDouble.CreateDbContext();	
			var sut = new PlantController(context, loggerMock.Object);
			var expected = Plants().SingleOrDefault(x => x.Id == id);

			// Act
			var result = await sut.Get(id);

			// Assert
			result.Should().BeOfType<ActionResult<Plant>>();
			result.Result.As<OkObjectResult>().Value.Should().BeEquivalentTo(expected);

		}

		[Fact]
		public async Task Get_with_invalid_id_returns_notfound_result()
		{
			//Arrange
			var loggerMock = new Mock<ILogger<PlantController>>();
			var context = ContextDouble.CreateDbContext();
	
			var sut = new PlantController(context, loggerMock.Object);

			// Act
			var result = await sut.Get(3);
			// Assert
			result.Result.Should().BeOfType<NotFoundResult>();
		}

		[Fact]
		public async Task Post_with_valid_cropdto_returns_ok_result()
		{
			//Arrange
			var loggerMock = new Mock<ILogger<PlantController>>();
			var context = ContextDouble.CreateDbContext();
			
			var sut = new PlantController(context, loggerMock.Object);
			var plant = new Plant()
			{
				Genus = Genus.Tree,
				GrowInWeeks = 12,
				Name = "Tree 1"
			};

			//act
			var result = await sut.Post(plant);

			//assert
			result.Should().BeOfType<OkResult>();
			context.Plants.Should().HaveCount(3);
		}

		[Fact]
		public async Task Post_with_invalid_cropdto_returns_badrequest_result()
		{
			//Arrange
			var loggerMock = new Mock<ILogger<PlantController>>();
			var context = ContextDouble.CreateDbContext();	
			var sut = new PlantController(context, loggerMock.Object);
			var plant = new Plant();

			//act
			var result = await sut.Post(plant);

			//assert
			result.Should().BeOfType<BadRequestResult>();
		}

		[Fact]
		public async Task Put_with_valid_cropdto_returns_ok_result()
		{
			//Arrange
			//Arrange
			var loggerMock = new Mock<ILogger<PlantController>>();
			var context = ContextDouble.CreateDbContext();
		
			var sut = new PlantController(context, loggerMock.Object);
			var plant = Plants().First();		
			plant.Genus = Genus.Other;
			plant.Name = "Cactus";

			//act
			var result = await sut.Put(plant);
			var actual = context.Plants.AsNoTracking().FirstOrDefault(x => x.Id ==plant.Id);

			//Assert
			result.Should().BeOfType<OkResult>();
			context.Plants.Should().HaveCount(2);
			actual.Should().BeEquivalentTo(plant);

		}

		[Fact]
		public async Task Put_with_invalid_cropdto_returns_badrequest_result()
		{
			//Arrange
			var loggerMock = new Mock<ILogger<PlantController>>();
			var context = ContextDouble.CreateDbContext();
			var sut = new PlantController(context, loggerMock.Object);

			//act
			var result = await sut.Put(new Plant());

			//assert
			result.Should().BeOfType<BadRequestResult>();
			context.Plants.Should().HaveCount(2);
		}

		[Fact]
		public async Task Delete_with_valid_id_Return_ok_result()
		{
			//Arrange
			var loggerMock = new Mock<ILogger<PlantController>>();
			var context = ContextDouble.CreateDbContext();
			var sut = new PlantController(context, loggerMock.Object);

			//act
			var result = await sut.Delete(1);

			//
			result.Should().BeOfType<OkResult>();
			context.Plants.Should().HaveCount(1);
		}

		[Fact]
		public async Task Delete_with_invalid_id_returns_notfound_result()
		{
			//Arrange
			var loggerMock = new Mock<ILogger<PlantController>>();
			var context = ContextDouble.CreateDbContext();
			var sut = new PlantController(context, loggerMock.Object);

			//act
			var result = await sut.Delete(3);

			//Assert
			result.Should().BeOfType<NotFoundObjectResult>();
			context.Plants.Should().HaveCount(2);
		}

		public static List<Plant> Plants()
		{
			return new List<Plant>
			{
				new Plant
				{
					Genus = Genus.Flower,
					GrowInWeeks = 5,
					Id = 1,
					Name = "Flower 1"
				},
				new Plant
				{
					Genus = Genus.Vegetable,
					GrowInWeeks = 21,
					Id = 2,
					Name = "Vegetable 1"
				}
			};
		}
	}
}
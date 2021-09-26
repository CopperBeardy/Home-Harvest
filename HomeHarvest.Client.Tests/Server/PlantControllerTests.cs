using FluentAssertions;
using HomeHarvest.Server.Controllers;
using HomeHarvest.Shared.Dtos;
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
		public async Task GetAll_reutrns_list_cropdtos()
		{
			//Arrange
			var loggerMock = new Mock<ILogger<PlantController>>();
			var context = ContextDouble.CreateDbContext();
			var mapper = MapperDouble.CreateMapper();
			var sut = new PlantController(context,loggerMock.Object, mapper);
			var expected = TestData.PlantDtoList();

			// Act
			var result = await sut.GetAll();

			// Assert
			result.Should().BeOfType<ActionResult<IEnumerable<PlantDto>>>();
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
			var mapper = MapperDouble.CreateMapper();
			var sut = new PlantController(context, loggerMock.Object, mapper);
			var expected = TestData.PlantDtoList();

			// Act
			var result = await sut.Get(id);

			// Assert
			result.Should().BeOfType<ActionResult<PlantDto>>();
			result.Result.As<OkObjectResult>().Value.Should().BeEquivalentTo(expected[id-1]);

		}

		[Fact]
		public async Task Get_with_invalid_id_returns_notfound_result()
		{
			//Arrange
			var loggerMock = new Mock<ILogger<PlantController>>();
			var context = ContextDouble.CreateDbContext();
			var mapper = MapperDouble.CreateMapper();
		var sut = new PlantController(context, loggerMock.Object, mapper);

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
			var mapper = MapperDouble.CreateMapper();
			var sut = new PlantController(context, loggerMock.Object, mapper);
			var plant = new PlantDto()
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
			var mapper = MapperDouble.CreateMapper();
			var sut = new PlantController(context, loggerMock.Object, mapper);
			var plant = new PlantDto();

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
			var mapper = MapperDouble.CreateMapper();
			var sut = new PlantController(context, loggerMock.Object, mapper);
			var plants = TestData.PlantDtoList();
			var plant = plants[0];
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
			var mapper = MapperDouble.CreateMapper();
			var sut = new PlantController(context, loggerMock.Object, mapper);

			//act
			var result = await sut.Put(new PlantDto());

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
			var mapper = MapperDouble.CreateMapper();
			var sut = new PlantController(context, loggerMock.Object, mapper);

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
			var mapper = MapperDouble.CreateMapper();
			var sut = new PlantController(context, loggerMock.Object, mapper);

			//act
			var result = await sut.Delete(3);

			//

			result.Should().BeOfType<NotFoundObjectResult>();
			context.Plants.Should().HaveCount(2);
		}

	}
}
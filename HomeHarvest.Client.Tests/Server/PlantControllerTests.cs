using FluentAssertions;
using FluentAssertions.Execution;
using HomeHarvest.Server.Controllers;
using HomeHarvest.Server.Data;
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

		PlantController sut;
		ApplicationDbContext context;
		public PlantControllerTests()
		{
			var loggerMock = new Mock<ILogger<PlantController>>();

			context = TestContext.GetDbContext();
			var mapper = TestMapper.GetTestMapper();

			 sut = new PlantController(context, loggerMock.Object, mapper);

		}
		[Fact]
		public async Task ReturnAllItems()
		{
			// Act
			var result = await sut.GetAll();

			// Assert
			result.Should().BeOfType<ActionResult<IEnumerable<PlantDto>>>();
			result.Result.As<OkObjectResult>().Value.Should().BeEquivalentTo(TestData.GetPlants());
		}

		[Fact]
		public async Task ReturnSpecificItemFromId()
		{
			// Act
			var result1 = await sut.Get(1);
			var result2 = await sut.Get(2);

			// Assert
			var expected = TestData.GetPlants();
			using (new AssertionScope())
			{
				result1.Should().BeOfType<ActionResult<PlantDto>>();
				result1.Result.As<OkObjectResult>().Value.Should().BeEquivalentTo(expected[0]);

				result2.Should().BeOfType<ActionResult<PlantDto>>();
				result2.Result.As<OkObjectResult>().Value.Should().BeEquivalentTo(expected[1]);
			}
		}

		[Fact]
		public async Task NotFoundIsReturnedIfItemNotFound()
		{
			// Act
			var result = await sut.Get(3);
			// Assert
			result.Result.Should().BeOfType<NotFoundResult>();
		}

		[Fact]
		public async Task InsertNewItemIntoDb()
		{
			var plant = new PlantDto()
			{
				Genus = Genus.Tree,
				GrowInWeeks = 12,
				Name = "Tree 1"
			};

			//act
			var response = await sut.Post(plant);
			//assert

			Assert.IsType<OkResult>(response);
			context.Plants.Should().HaveCount(3);
		}

		[Fact]
		public async Task ReturnBadRequestInsertNewItemIntoDb()
		{
			var plant = new PlantDto();
		

			//act
			var response = await sut.Post(plant);
			//assert

			Assert.IsType<BadRequestResult>(response);
			
		}

		[Fact]
		public async Task UpdateItemInDb()
		{
			//arrange
			var plants = TestData.GetPlants();
			var plant = plants[0];
			plant.Genus = Genus.Other;
			plant.Name = "Cactus";

			//act
			var response = await sut.Put(plant);
			var actual = context.Plants.AsNoTracking().FirstOrDefault(x => x.Id ==plant.Id);

			//Assert
			Assert.IsType<OkResult>(response);
			context.Plants.Should().HaveCount(2);
			Assert.Equal(Genus.Other, actual.Genus);
			Assert.Equal("Cactus", actual.Name);
		}

		[Fact]
		public async Task BadRequestResultFromUpdateWhenBadEntity()
		{
			//arrange
			var plant = new PlantDto
			{
				Genus = Genus.Tree,
				GrowInWeeks = 1,
				Id = 4,
				Name = "Bad entity"
			};
			//act
			var response = await sut.Put(plant);

			//assert
			Assert.IsType<BadRequestResult>(response);
			context.Plants.Should().HaveCount(2);
		}

		[Fact]
		public async Task RemoveItemForDatabase()
		{
			//act
			var response = await sut.Delete(1);

			//
			Assert.IsType<OkResult>(response);
			context.Plants.Should().HaveCount(1);
		}

		[Fact]
		public async Task NotFoundResultWhenItemForDeletionDoesNotExist()
		{
			//act
			var response = await sut.Delete(3);

			//
			Assert.IsType<NotFoundObjectResult>(response);
			context.Plants.Should().HaveCount(2);
		}

	}
}
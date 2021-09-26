using FluentAssertions;
using HomeHarvest.Server.Controllers;
using HomeHarvest.Shared.Entities;
using HomeHarvest.Shared.Enums;
using HomeHarvestTests.TestHelpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Xunit;


namespace HomeHarvestTests.Server
{
	[ExcludeFromCodeCoverage]
	public class SownControllerTests
	{

		[Fact]
		public async Task GetAll_return_List_Sowns()
		{
			//Arrange
			var loggerMock = new Mock<ILogger<SownController>>();
			var context = ContextDouble.CreateDbContext();
			var sut = new SownController(context, loggerMock.Object);
			var expected = SownWithoutPlants(); 
			// Act
			var result = await sut.GetAll();

			// Assert
			result.Should().BeOfType<ActionResult<IEnumerable<Sown>>>();
			result.Result.As<OkObjectResult>().Value.Should().BeEquivalentTo(expected);
		}

		[Theory]
		[InlineData(1)]
		[InlineData(2)]
		public async Task Get_with_valid_id_returns_Sown(int id)
		{
			//Arrange
			var loggerMock = new Mock<ILogger<SownController>>();
			var context = ContextDouble.CreateDbContext();		
			var sut = new SownController(context, loggerMock.Object);
			var expected = SownWithPlants().SingleOrDefault(x => x.Id == id);

			// Act
			var result = await sut.Get(id);

			// Assert
			result.Should().BeOfType<ActionResult<Sown>>();
			result.Result.As<OkObjectResult>().Value.Should().BeEquivalentTo(expected);
		}

		[Fact]
		public async Task Get_with_invalid_id_returns_notfound_result()
		{
			//Arrange
			var loggerMock = new Mock<ILogger<SownController>>();
			var context = ContextDouble.CreateDbContext();
		
			var sut = new SownController(context, loggerMock.Object);
			// Act
			var result = await sut.Get(3);
			// Assert
			result.Result.Should().BeOfType<NotFoundResult>();
		}


		public static List<Sown> SownWithoutPlants()
		{
			return new List<Sown>()
			{
				new Sown
				{
					CropId =1,
					Id =1,
					PlantId= 1,
					PlantedOn = DateTime.Today.AddDays(-2),
					PoiX = 23,
					PoiY = 23,
				},
				new Sown
				{
					CropId = 1,
					Id = 2,
					PlantId = 2,
					PlantedOn = DateTime.Today,
					PoiX = 26,
					PoiY = 26,
				}
			};
		}

		public static List<Sown> SownWithPlants()
		{
			return new List<Sown>()
			{
				new Sown
				{
					CropId =1,
					Id =1,
					PlantId= 1,
					Plant = new Plant()
					{
						Genus = Genus.Flower,
						GrowInWeeks = 5,
						Id = 1,
						Name = "Flower 1"
					},
					PlantedOn = DateTime.Today.AddDays(-2),
					PoiX = 23,
					PoiY = 23,
				},
				new Sown
				{
					CropId = 1,
					Id = 2,
					PlantId = 2,
					Plant = new Plant()
					{
						Genus = Genus.Vegetable,
						GrowInWeeks = 21,
						Id = 2,
						Name = "Vegetable 1"
					},
					PlantedOn = DateTime.Today,
					PoiX = 26,
					PoiY = 26,
				}
			};
		}
	}
}

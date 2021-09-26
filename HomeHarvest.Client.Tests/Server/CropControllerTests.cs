using FluentAssertions;
using HomeHarvest.Server.Controllers;
using HomeHarvest.Server.Services;
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
	public class CropControllerTests
	{

		[Fact]
		public async Task GetAll_without_sowed_list_returns_list_crops()
		{
			//Arrange
			var loggerMock = new Mock<ILogger<CropController>>();
			var context = ContextDouble.CreateDbContext();
			var blobServiceMock = new Mock<IBlobService>();
			var sut = new CropController(context, loggerMock.Object, blobServiceMock.Object);
			var expected = new List<Crop>() {
				new Crop
				{
					Id = 1,
					Location = "Home",
					PlotImage = "Home.png",
					Year = 2021
				},
				new Crop
				{
					Id = 2,
					Location = "allotment",
					PlotImage = "Allotment.png",
					Year = 2021,
				}
			};
			// Act
			var result = await sut.GetAll();

			// Assert
			result.Should().BeOfType<ActionResult<IEnumerable<Crop>>>();
			result.Result.As<OkObjectResult>().Value.Should().BeEquivalentTo(expected);
		}

		[Theory]
		[InlineData(1)]
		[InlineData(2)]
		public async Task Get_with_valid_id_returns_crop(int id)
		{
			//Arrange
			var loggerMock = new Mock<ILogger<CropController>>();
			var context = ContextDouble.CreateDbContext();
			var blobServiceMock = new Mock<IBlobService>();
			var sut = new CropController(context, loggerMock.Object, blobServiceMock.Object);
			var crops = new List<Crop>()
			{
				new Crop
				{
					Id = 1,
					Location = "Home",
					PlotImage = "Home.png",
					Year = 2021,
					Sowed = new List<Sown>()
					{
						new Sown
						{
							CropId = 1,
							Id = 1,
							PlantId = 1,
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
						},
					}
				},
				new Crop
				{
					Id = 2,
					Location = "allotment",
					PlotImage = "Allotment.png",
					Year = 2021,
					Sowed = new List<Sown>()
				}
			};
			var expected = crops.SingleOrDefault(x => x.Id == id);

			// Act
			var result = await sut.Get(id);
			var actual = result.Result.As<OkObjectResult>().Value;

			// Assert
			actual.Should().BeOfType<Crop>();
			actual.Should().BeEquivalentTo(expected);
		}

		[Fact]
		public async Task Get_with_invalid_id_returns_notfound_result()
		{
			//Arrange
			var loggerMock = new Mock<ILogger<CropController>>();
			var context = ContextDouble.CreateDbContext();
			var blobServiceMock = new Mock<IBlobService>();
			var sut = new CropController(context, loggerMock.Object, blobServiceMock.Object);
			// Act
			var result = await sut.Get(3);
			// Assert
			result.Result.Should().BeOfType<NotFoundResult>();
		}

		[Fact]
		public async Task Post_with_crop_returns_ok_result()
		{
			//Arrange
			var loggerMock = new Mock<ILogger<CropController>>();
			var context = ContextDouble.CreateDbContext();
			var blobServiceMock = new Mock<IBlobService>();
			var sut = new CropController(context, loggerMock.Object, blobServiceMock.Object);
			var crop = new Crop()
			{
				Image = new byte[] { 1, 2, 3 },
				Location = "Insert 1",
				PlotImage = "Insert.png",
				Year = 2022
			};

			var response = await sut.Post(crop);

			Assert.IsType<OkResult>(response);
			context.Crops.Should().HaveCount(3);
		}

		[Fact]
		public async Task Post_with_invalid_crop_reuturns_badrequest_result()
		{
			//Arrange
			var loggerMock = new Mock<ILogger<CropController>>();
			var context = ContextDouble.CreateDbContext();
			var blobServiceMock = new Mock<IBlobService>();
			var sut = new CropController(context, loggerMock.Object, blobServiceMock.Object);
			sut.ModelState.AddModelError("no image", "no imagr provide for testing");

			//Act
			var result = await sut.Post(new Crop());

			//Assert
			result.Should().BeOfType<BadRequestResult>();
		}

	}
}

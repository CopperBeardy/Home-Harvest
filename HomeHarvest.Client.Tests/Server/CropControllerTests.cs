using FluentAssertions;
using HomeHarvest.Server.Controllers;
using HomeHarvest.Server.Services;
using HomeHarvest.Shared.Dtos;
using HomeHarvestTests.TestHelpers;
using Microsoft.AspNetCore.Mvc;
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
	public  class CropControllerTests
	{

		[Fact]
		public async Task GetAll_without_sowed_list_returns_list_cropdtos()
		{
			//Arrange
			var loggerMock = new Mock<ILogger<CropController>>();
			var context = ContextDouble.CreateDbContext();
			var mapper = MapperDouble.CreateMapper();
			var blobServiceMock = new Mock<IBlobService>();
			var sut = new CropController(context, loggerMock.Object, mapper, blobServiceMock.Object);

			// Act
			var result = await sut.GetAll();

			// Assert
			result.Should().BeOfType<ActionResult<IEnumerable<CropDto>>>();
			result.Result.As<OkObjectResult>().Value.Should().BeEquivalentTo(TestData.CropDtoList());
		}

		[Fact]
		public async Task Get_with_valid_id_returns_cropDto_with_sowed_list()
		{
			//Arrange
			var loggerMock = new Mock<ILogger<CropController>>();
			var context = ContextDouble.CreateDbContext();
			var mapper = MapperDouble.CreateMapper();
			var blobServiceMock = new Mock<IBlobService>();
			var sut = new CropController(context, loggerMock.Object, mapper, blobServiceMock.Object);
			var expected = GetCropWithSown(1);

			// Act
			var result = await sut.Get(1);
			var actual = result.Result.As<OkObjectResult>().Value;
			// Assert	
			actual.Should().BeOfType<CropDto>();
			actual.Should().BeEquivalentTo(expected);
			//Assert.IsType<CropDto>(actual);
			//Assert.Equal(expected.Id, actual.Id);
		}

		[Fact]
		public async Task Get_with_valid_id_returns_cropDto_without_sowed_list()
		{
			//Arrange
			var loggerMock = new Mock<ILogger<CropController>>();
			var context = ContextDouble.CreateDbContext();
			var mapper = MapperDouble.CreateMapper();
			var blobServiceMock = new Mock<IBlobService>();
			var sut = new CropController(context, loggerMock.Object, mapper, blobServiceMock.Object);
			var expected = TestData.CropDtoList().SingleOrDefault(x => x.Id == 2);

			// Act
			var result = await sut.Get(2);
			var actual = result.Result.As<OkObjectResult>().Value;
			// Assert	
			actual.Should().BeOfType<CropDto>();
			actual.Should().BeEquivalentTo(expected);
			//Assert.IsType<CropDto>(actual);
			//Assert.Equal(expected.Id, actual.Id);
		}

		[Fact]
		public async Task Get_with_invalid_id_returns_notfound_result()
		{
			//Arrange
			var loggerMock = new Mock<ILogger<CropController>>();
			var context = ContextDouble.CreateDbContext();
			var mapper = MapperDouble.CreateMapper();
			var blobServiceMock = new Mock<IBlobService>();
			var sut = new CropController(context, loggerMock.Object, mapper, blobServiceMock.Object);
			// Act
			var result = await sut.Get(3);
			// Assert
			result.Result.Should().BeOfType<NotFoundResult>();
		}

		[Fact]
		public async Task Post_with_cropdto_returns_ok_result()
		{
			//Arrange
			var loggerMock = new Mock<ILogger<CropController>>();
			var context = ContextDouble.CreateDbContext();
			var mapper = MapperDouble.CreateMapper();
			var blobServiceMock = new Mock<IBlobService>();
			var sut = new CropController(context, loggerMock.Object, mapper, blobServiceMock.Object);
			var crop = new CropDto()
			{ 
				 Image = new byte[] {  1, 2, 3 },
				Location = "Insert 1",
				PlotImage= "Insert.png",
				Year = 2022
			};

			var response = await sut.Post(crop);

			Assert.IsType<OkResult>(response);
			context.Crops.Should().HaveCount(3);
		}

		[Fact]
		public async Task Post_with_invalid_cropdto_reuturns_badrequest_result()
		{
			//Arrange
			var loggerMock = new Mock<ILogger<CropController>>();
			var context = ContextDouble.CreateDbContext();
			var mapper = MapperDouble.CreateMapper();
			var blobServiceMock = new Mock<IBlobService>();
			var sut = new CropController(context, loggerMock.Object, mapper, blobServiceMock.Object);
			sut.ModelState.AddModelError("no image", "no imagr provide for testing");

			//Act
			var result = await sut.Post(new CropDto());

			//Assert
			result.Should().BeOfType<BadRequestResult>();
		}

		public static CropDto GetCropWithSown(int id)
		{
			var crop = TestData.CropDtoList().FirstOrDefault(x => x.Id == id);

			crop.Sowed = GetSownWithPlants(crop);

			return crop;
		}

		public static List<SownDto> GetSownWithPlants(CropDto crop)
		{
			var plants = TestData.PlantDtoList();
			var sown = TestData.SownDtoList();
			foreach (var s in sown)
			{
				s.Plant = plants.SingleOrDefault(x => x.Id == s.Id);
				s.Crop = crop;
			}
			return sown;
		}
	}
}

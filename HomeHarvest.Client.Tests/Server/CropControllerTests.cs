using FluentAssertions;
using FluentAssertions.Execution;
using HomeHarvest.Server.Controllers;
using HomeHarvest.Server.Data;
using HomeHarvest.Server.Services;
using HomeHarvest.Shared.Dtos;
using HomeHarvest.Shared.Enums;
using HomeHarvestTests.TestHelpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
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
	public  class CropControllerTests
	{
		CropController sut;
		ApplicationDbContext context;

		public CropControllerTests()
		{
			var loggerMock = new Mock<ILogger<CropController>>();

			context = TestContext.GetDbContext();
			var mapper = TestMapper.GetTestMapper();
			var blobServiceMock = new Mock<IBlobService>();

			sut = new CropController(context, loggerMock.Object, mapper, blobServiceMock.Object);
		}

		[Fact]
		public async Task ReturnAllItems()
		{
			// Act
			var result = await sut.GetAll();

			// Assert
			result.Should().BeOfType<ActionResult<IEnumerable<CropDto>>>();
			result.Result.As<OkObjectResult>().Value.Should().BeEquivalentTo(TestData.GetCrops());
		}

		[Theory]
		[InlineData(1)]
		[InlineData(2)]
		public async Task ReturnSpecificItemFromId(int id)
		{
			// Act
			var result = await sut.Get(id);

			// Assert
			var expected = TestData.GetCropWithSown(id);

			result.Should().BeOfType<ActionResult<CropDto>>();
			var actual = result.Result.As<OkObjectResult>().Value as CropDto;
			Assert.IsType<CropDto>(actual);
			Assert.Equal(expected.Id, actual.Id);	
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
		public async Task InsertNewIntoDb()
		{
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
		public async Task BadRequestReturnWhenExceptionThrown()
		{
			sut.ModelState.AddModelError("no image", "no imagr provide for testing");
			var result = await sut.Post(new CropDto());
			result.Should().BeOfType<BadRequestResult>();
		}
	}
}

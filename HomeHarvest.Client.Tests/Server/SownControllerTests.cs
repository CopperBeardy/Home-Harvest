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
	public  class SownControllerTests
	{
		SownController sut;
		ApplicationDbContext context;

		public SownControllerTests()
		{
			var loggerMock = new Mock<ILogger<SownController>>();

			context = TestContext.GetDbContext();
			var mapper = TestMapper.GetTestMapper();

			sut = new SownController(context, loggerMock.Object, mapper);
		}

		[Fact]
		public async Task ReturnAllItems()
		{
			// Act
			var result = await sut.GetAll();

			// Assert
			result.Should().BeOfType<ActionResult<IEnumerable<SownDto>>>();
			result.Result.As<OkObjectResult>().Value.Should().BeEquivalentTo(TestData.GetSown());
		}

		[Fact]
		public async Task ReturnSpecificItemFromId()
		{
			// Act
			var result1 = await sut.Get(1);
			var result2 = await sut.Get(2);

			// Assert
			var expected = TestData.GetSownWithPlants();
			using (new AssertionScope())
			{
				result1.Should().BeOfType<ActionResult<SownDto>>();
				result1.Result.As<OkObjectResult>().Value.Should().BeEquivalentTo(expected[0]);

				result2.Should().BeOfType<ActionResult<SownDto>>();
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
	}
}

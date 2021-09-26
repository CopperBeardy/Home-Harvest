using FluentAssertions;
using HomeHarvest.Server.Controllers;
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
	public  class SownControllerTests
	{

		[Fact]
		public async Task GetAll_return_List_sowndtos()
		{
			//Arrange
			var loggerMock = new Mock<ILogger<SownController>>();
			var context = ContextDouble.CreateDbContext();
			var mapper = MapperDouble.CreateMapper();
			var sut = new SownController(context, loggerMock.Object, mapper);

			// Act
			var result = await sut.GetAll();

			// Assert
			result.Should().BeOfType<ActionResult<IEnumerable<SownDto>>>();
			result.Result.As<OkObjectResult>().Value.Should().BeEquivalentTo(TestData.SownDtoList());
		}

		[Theory]
		[InlineData(1)]
		[InlineData(2)]
		public async Task Get_with_valid_id_returns_sowndto(int id)
		{
			//Arrange
			var loggerMock = new Mock<ILogger<SownController>>();
			var context = ContextDouble.CreateDbContext();
			var mapper = MapperDouble.CreateMapper();
			var sut = new SownController(context, loggerMock.Object, mapper);
			var expected = GetSownWithPlants();

			// Act
			var result = await sut.Get(id);

			// Assert
			result.Should().BeOfType<ActionResult<SownDto>>();
			result.Result.As<OkObjectResult>().Value.Should().BeEquivalentTo(expected[id-1]);
		}

		[Fact]
		public async Task Get_with_invalid_id_returns_notfound_result()
		{
			//Arrange
			var loggerMock = new Mock<ILogger<SownController>>();
			var context = ContextDouble.CreateDbContext();
			var mapper = MapperDouble.CreateMapper();
			var sut = new SownController(context, loggerMock.Object, mapper);
			// Act
			var result = await sut.Get(3);
			// Assert
			result.Result.Should().BeOfType<NotFoundResult>();
		}

		public static List<SownDto> GetSownWithPlants()
		{
			var plants = TestData.PlantDtoList();
			var sown = TestData.SownDtoList();
			foreach (var s in sown)
			{
				s.Plant = plants.SingleOrDefault(x => x.Id == s.Id);
			}
			return sown;
		}
	}
}

using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Moq;
using HomeHarvest.Server.Controllers;
using HomeHarvest.Shared.Dtos;
using HomeHarvest.Server.Entities;
using Microsoft.Extensions.Logging;
using AutoMapper;
using FluentAssertions;
using HomeHarvest.Client.Services;
using Microsoft.AspNetCore.Mvc;
using FluentAssertions.Execution;

namespace HomeHarvestTests.Server
{
    public class PlantControllerTests
    {
        [Fact]
        public async Task ReturnAllItems()
        {
            // Arrange 
            var repoMock = new Mock<IRepository<Plant>>();
            repoMock.Setup(_ => _.GetAll()).ReturnsAsync(GetPlants());

            var loggerMock = new Mock<ILogger<PlantController>>();

            var mapperMock = new Mock<IMapper>();

            var sut = new PlantController(repoMock.Object, loggerMock.Object, mapperMock.Object);

            // Act
            var result = await sut.GetAll();

            // Assert 
            var expected = new List<PlantDto>
            {
                new PlantDto
                {
                    Genus = HomeHarvest.Shared.Enums.Genus.Flower,
                    GrowInWeeks= 5,
                    Id= 1,
                    Name = "Flower 1"
                },
                new PlantDto
                {
                    Genus = HomeHarvest.Shared.Enums.Genus.Vegetable,
                    GrowInWeeks = 21,
                    Id = 2,
                    Name = "Vegetable 1"
                }
            };

            result.Should().BeOfType<ActionResult<IEnumerable<PlantDto>>>();
            result.Result.As<OkObjectResult>().Value.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task ReturnSpecificItemFromId()
        {
            // Arrange
            var plants = GetPlants();
            var repoMock = new Mock<IRepository<Plant>>();
            repoMock.Setup(_ => _.GetById(1)).ReturnsAsync(plants[0]);
            repoMock.Setup(_ => _.GetById(2)).ReturnsAsync(plants[1]);

            var loggerMock = new Mock<ILogger<PlantController>>();

            var mapperMock = new Mock<IMapper>();

            var sut = new PlantController(repoMock.Object, loggerMock.Object, mapperMock.Object);

            // Act
            var result1 = await sut.Get(1);
            var result2 = await sut.Get(2);

            // Assert  
            using (new AssertionScope())
            {
                result1.Should().BeOfType<ActionResult<PlantDto>>();
                result1.Result.As<OkObjectResult>().Value.Should().BeEquivalentTo(
                    new PlantDto
                    {
                        Genus = HomeHarvest.Shared.Enums.Genus.Flower,
                        GrowInWeeks = 5,
                        Id = 1,
                        Name = "Flower 1"
                    });

                result2.Should().BeOfType<ActionResult<PlantDto>>();
                result2.Result.As<OkObjectResult>().Value.Should().BeEquivalentTo(
                    new PlantDto
                    {
                        Genus = HomeHarvest.Shared.Enums.Genus.Vegetable,
                        GrowInWeeks = 21,
                        Id = 2,
                        Name = "Vegetable 1"
                    });
            }
        }

        [Fact]
        public async Task NotFoundIsReturnedIfItemNotFound()
        {
            // Arrange
            var plants = GetPlants();
            var repoMock = new Mock<IRepository<Plant>>();
            repoMock.Setup(_ => _.GetById(1)).ReturnsAsync(plants[0]);
            repoMock.Setup(_ => _.GetById(2)).ReturnsAsync(plants[1]);

            var loggerMock = new Mock<ILogger<PlantController>>();

            var mapperMock = new Mock<IMapper>();

            var sut = new PlantController(repoMock.Object, loggerMock.Object, mapperMock.Object);

            // Act
            var result = await sut.Get(3);

            // Assert
            result.Result.Should().BeOfType<NotFoundResult>();
        }

        private static List<Plant> GetPlants()
        {
            return new List<Plant>
            {
                new Plant
                {
                    Genus = HomeHarvest.Shared.Enums.Genus.Flower,
                    GrowInWeeks = 5,
                    Id = 1,
                    Name = "Flower 1"
                },
                new Plant
                {
                    Genus = HomeHarvest.Shared.Enums.Genus.Vegetable,
                    GrowInWeeks = 21,
                    Id = 2,
                    Name = "Vegetable 1"
                }
            };
        }
    }
}
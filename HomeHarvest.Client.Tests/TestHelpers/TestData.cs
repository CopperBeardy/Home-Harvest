using HomeHarvest.Shared.Dtos;
using HomeHarvest.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace HomeHarvestTests.TestHelpers
{
	[ExcludeFromCodeCoverage]
	public static class TestData
	{
		public static List<PlantDto> PlantDtoList()
		{
			return new List<PlantDto>
			{
				new PlantDto
				{
					Genus = Genus.Flower,
					GrowInWeeks = 5,
					Id = 1,
					Name = "Flower 1"
				},
				new PlantDto
				{
					Genus = Genus.Vegetable,
					GrowInWeeks = 21,
					Id = 2,
					Name = "Vegetable 1"
				}
			};
		}



		public static List<CropDto> CropDtoList()
		{
			return new List<CropDto>
			{
				new CropDto
				{
					Id=1,
					Location = "Home",
					PlotImage = "Home.png",
					Year = 2021,
					Sowed = new List<SownDto>()
				},
				new CropDto
				{
					Id=2,
					Location = "allotment",
					PlotImage = "Allotment.png",
					Year = 2021,
					Sowed = new List<SownDto>()
				}
			};
		}


		public static List<SownDto> SownDtoList()
		{
			return new List<SownDto>()
			{
				new SownDto
				{
					CropId =1,
					Id =1,
					PlantId= 1,
					PlantedOn = DateTime.Today.AddDays(-2),
					PoiX = 23,
					PoiY = 23,
				},
				new SownDto
				{
					CropId =1,
					Id =2,
					PlantId= 2,
					PlantedOn = DateTime.Today,
					PoiX = 26,
					PoiY = 26,
				},
			};
		}
	}
}


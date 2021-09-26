using Duende.IdentityServer.EntityFramework.Options;
using HomeHarvest.Server.Data;
using HomeHarvest.Server.Entities;
using HomeHarvest.Shared.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace HomeHarvestTests.TestHelpers
{
	[ExcludeFromCodeCoverage]
	public static class ContextDouble
	{
		public static ApplicationDbContext CreateDbContext()
		{
			var options = new DbContextOptionsBuilder<ApplicationDbContext>()
				.UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
				.Options;
			var storeOptions = Options.Create(new OperationalStoreOptions());

			var databaseContext = new ApplicationDbContext(options, storeOptions);
			databaseContext.Database.EnsureCreated();
			if (databaseContext.Users.Count() <= 0)
			{

				databaseContext.Plants.Add(new Plant
				{
					Genus = Genus.Flower,
					GrowInWeeks = 5,
					Id = 1,
					Name = "Flower 1"
				});
				databaseContext.Add(
				new Plant
				{
					Genus =Genus.Vegetable,
					GrowInWeeks = 21,
					Id = 2,
					Name = "Vegetable 1"
				});
				databaseContext.SaveChanges();
				databaseContext.Crops.Add(
					new Crop
					{
						Id=1,
						Location = "Home",
						PlotImage = "Home.png",
						Year = 2021,
						Sowed =	new List<Sown>()
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
								 CropId =1,
							 Id =2,
							 PlantId= 2,
							 PlantedOn = DateTime.Today,
							 PoiX = 26,
							 PoiY = 26,
							},
						}
					}	);
				databaseContext.Crops.Add(
					new Crop()
					{
						Id = 2,
						Location = "allotment",
						PlotImage = "Allotment.png",
						Year = 2021
					});
				databaseContext.SaveChanges();

			}
			databaseContext.ChangeTracker.Clear();
			return databaseContext;
		}
	}
}

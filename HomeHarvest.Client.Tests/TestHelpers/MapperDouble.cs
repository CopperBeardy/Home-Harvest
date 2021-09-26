using AutoMapper;
using HomeHarvest.Server.Profiles;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeHarvestTests.TestHelpers
{
	[ExcludeFromCodeCoverage]
	public static  class MapperDouble
	{
		public static IMapper CreateMapper()
		{
				return new MapperConfiguration(mc =>
					mc.AddProfile(new AutoMapperProfiles()))
				.CreateMapper();
		}
	}
}

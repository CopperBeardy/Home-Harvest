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
	public static  class TestMapper
	{
		public static IMapper GetTestMapper()
		{
				var mapConfig = new MapperConfiguration(mc =>
					mc.AddProfile(new AutoMapperProfiles())
				);
				IMapper mapper = mapConfig.CreateMapper();
				return mapper;
		}
	}
}

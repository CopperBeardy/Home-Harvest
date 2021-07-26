using HomeHarvest.Server.Entities;
using HomeHarvest.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace HomeHarvest.Server.Profiles
{
	public class AutoMapperProfiles :Profile
	{
		public AutoMapperProfiles()
		{
			CreateMap<Crop,CropDto>().ReverseMap();
			CreateMap<Sow,SowDto>().ReverseMap();
			CreateMap<Plant,PlantDto>().ReverseMap();
		}
	
	}
}

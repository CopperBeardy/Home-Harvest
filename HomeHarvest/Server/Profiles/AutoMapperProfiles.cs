using AutoMapper;
using HomeHarvest.Server.Entities;
using HomeHarvest.Shared.Dtos;

namespace HomeHarvest.Server.Profiles
{
	public class AutoMapperProfiles : Profile
	{
		public AutoMapperProfiles()
		{
			CreateMap<Crop, CropDto>().ReverseMap();
			CreateMap<CreateCropDto, Crop>();
			CreateMap<Sow, SowDto>().ReverseMap();
			CreateMap<Plant, PlantDto>().ReverseMap();
		}

	}
}

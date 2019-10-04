using AutoMapper;
using CAM.Core.Entities;
using CAM.Web.ApiModels;

namespace CAM.Web
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // Aircraft -> AircraftDto
            CreateMap<Aircraft, AircraftDto>()
                .ForMember(dest => dest.Times, opt => opt.MapFrom(src => src.Times))
                .ForMember(dest => dest.Squawks, opt => opt.MapFrom(src => src.Squawks))
                .ReverseMap();
        }
    }
}
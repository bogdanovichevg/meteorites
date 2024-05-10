using Application.DTO;
using AutoMapper;
using Domain.Entity;
using Infrastructure.DTO;
using Newtonsoft.Json;

namespace Application.MappingProfile
{
    public class MeteoriteProfile : Profile
    {
        public MeteoriteProfile()
        {
            CreateMap<MeteoriteDTO, Meteorite>()
                .ForMember(dest => dest.GeoLocation, opt => opt.MapFrom(src => JsonConvert.SerializeObject(src.GeoLocation)))
                .ForMember(dest => dest.Year, opt => opt.MapFrom(src => src.Year.Year));
            CreateMap<Meteorite, MeteoriteDTO>()
                .ForMember(dest => dest.GeoLocation, opt => opt.MapFrom(src => JsonConvert.DeserializeObject<GeoLocation>(src.GeoLocation)))
                .ForMember(dest => dest.Year, opt => opt.MapFrom(src => new DateTime(src.Year, 0, 0)));
            CreateMap<ReqFilterMeteorites, FiltersMeteoritesInfo>()
                .ReverseMap();
        }
    }
}

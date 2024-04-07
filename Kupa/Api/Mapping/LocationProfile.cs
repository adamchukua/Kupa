using AutoMapper;
using Kupa.Api.Dtos;
using Kupa.Api.Models;

namespace Kupa.Api.Mapping
{
    public class LocationProfile : Profile
    {
        public LocationProfile()
        {
            CreateMap<NewEventDto, Location>()
                .ForSourceMember(source => source.Title, dest => dest.DoNotValidate())
                .ForSourceMember(source => source.Description, dest => dest.DoNotValidate())
                .ForSourceMember(source => source.Location, dest => dest.DoNotValidate())
                .ForMember(dest => dest.TypeId, source => source.MapFrom(src => src.LocationTypeId))
                .AfterMap((src, dest) =>
                {
                    switch (src.LocationTypeId)
                    {
                        case Enums.LocationTypeId.Offline:
                            dest.Address = src.Location;
                            break;
                        case Enums.LocationTypeId.Online:
                            dest.Url = src.Location;
                            break;
                    }
                });
            CreateMap<UpdateEventDto, Location>()
                .ForSourceMember(source => source.Title, dest => dest.DoNotValidate())
                .ForSourceMember(source => source.Description, dest => dest.DoNotValidate())
                .ForSourceMember(source => source.Location, dest => dest.DoNotValidate())
                .ForMember(dest => dest.TypeId, source => source.MapFrom(src => src.LocationTypeId))
                .AfterMap((src, dest) =>
                {
                    switch (src.LocationTypeId)
                    {
                        case Enums.LocationTypeId.Offline:
                            dest.Address = src.Location;
                            break;
                        case Enums.LocationTypeId.Online:
                            dest.Url = src.Location;
                            break;
                    }
                });
        }
    }
}

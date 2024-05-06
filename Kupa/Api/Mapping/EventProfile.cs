using AutoMapper;
using Kupa.Api.Dtos;
using Kupa.Api.Enums;
using Kupa.Api.Models;

namespace Kupa.Api.Mapping
{
    public class EventProfile : Profile
    {
        public EventProfile()
        {
            CreateMap<EventDto, Event>();

            CreateMap<NewEventDto, Event>()
                .ForMember(dest => dest.Location, opt => opt.MapFrom(src => new Location
                {
                    TypeId = src.LocationTypeId,
                    Address = src.LocationTypeId == LocationTypeId.Offline ? src.Location : null,
                    Url = src.LocationTypeId == LocationTypeId.Online ? src.Location : null,
                    CityId = src.CityId
                }))
                .ForMember(dest => dest.EventSurveyQuestions, opt => opt.MapFrom(src => src.SurveyQuestions));

            CreateMap<UpdateEventDto, Event>()
                .ForMember(dest => dest.Location, opt => opt.MapFrom(src => new Location
                {
                    TypeId = src.LocationTypeId,
                    Address = src.LocationTypeId == LocationTypeId.Offline ? src.Location : null,
                    Url = src.LocationTypeId == LocationTypeId.Online ? src.Location : null,
                    CityId = src.CityId
                }))
                .ForMember(dest => dest.EventSurveyQuestions, opt => opt.MapFrom(src => src.SurveyQuestions));
        }
    }
}

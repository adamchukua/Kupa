using AutoMapper;
using Kupa.Api.Dtos;
using Kupa.Api.Models;

namespace Kupa.Api.Mapping
{
    public class EventProfile : Profile
    {
        public EventProfile()
        {
            CreateMap<EventDto, Event>();
        }
    }
}

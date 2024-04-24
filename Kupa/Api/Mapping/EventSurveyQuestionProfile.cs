using AutoMapper;
using Kupa.Api.Dtos;
using Kupa.Api.Models;

namespace Kupa.Api.Mapping
{
    public class EventSurveyQuestionProfile : Profile
    {
        public EventSurveyQuestionProfile()
        {
            CreateMap<SurveyQuestionDto, EventSurveyQuestion>();
        }
    }
}

using AutoMapper;
using Kupa.Api.Dtos;
using Kupa.Api.Models;

namespace Kupa.Api.Mapping
{
    public class EventSurveyAnswerProfile : Profile
    {
        public EventSurveyAnswerProfile()
        {
            CreateMap<SurveyAnswerDto, EventSurveyAnswer>()
                .ForMember(dest => dest.EventSurveyQuestionId, opt => opt.MapFrom(src => src.QuestionId));
        }
    }
}

using AutoMapper;
using Kupa.Api.Dtos;
using Kupa.Api.Models;

namespace Kupa.Api.Mapping
{
    public class SurveyQuestionProfile : Profile
    {
        public SurveyQuestionProfile()
        {
            CreateMap<SurveyQuestionDto, EventSurveyQuestion>();
        }
    }
}

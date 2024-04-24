using AutoMapper;
using Kupa.Api.Dtos;
using Kupa.Api.Models;
using Kupa.Api.Services.Implementations;
using Kupa.Api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kupa.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventSurveyAnswerController : ControllerBase
    {
        private readonly IEventSurveyAnswerService _eventSurveyAnswerService;
        private readonly IMapper _mapper;

        public EventSurveyAnswerController(IEventSurveyAnswerService eventSurveyAnswerService, IMapper mapper)
        {
            _eventSurveyAnswerService = eventSurveyAnswerService;
            _mapper = mapper;
        }

        [HttpPost("event/{eventId}")]
        [Authorize]
        public async Task<IActionResult> AddAnswers(int eventId, SurveyAnswerDto[] answersDto)
        {
            if (answersDto == null)
            {
                return BadRequest("Answers is null or empty.");
            }

            EventSurveyAnswer[] eventSurveyAnswers = _mapper.Map<EventSurveyAnswer[]>(answersDto);

            await _eventSurveyAnswerService.AddAnswersAsync(eventId, eventSurveyAnswers);
            return NoContent();
        }
    }
}

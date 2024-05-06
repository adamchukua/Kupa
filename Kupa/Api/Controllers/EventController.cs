using AutoMapper;
using Kupa.Api.Dtos;
using Kupa.Api.Models;
using Kupa.Api.Repositories.Interfaces;
using Kupa.Api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kupa.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IEventService _eventService;
        private readonly IRegistrationService _registrationService;
        private readonly IExcelExportService _excelExportService;
        private readonly IMapper _mapper;

        public EventsController(
            IEventService eventService, 
            IRegistrationService registrationService,
            IExcelExportService excelExportService,
            IMapper mapper)
        {
            _eventService = eventService;
            _registrationService = registrationService;
            _excelExportService = excelExportService;
            _mapper = mapper;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateEvent(NewEventDto newEventDto)
        {
            if (newEventDto == null)
            {
                return BadRequest("Event data is null.");
            }

            Event eventObject = _mapper.Map<Event>(newEventDto);

            await _eventService.CreateEventAsync(eventObject);
            return Ok(eventObject);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEvents()
        {
            IEnumerable<Event> events = await _eventService.GetAllEventsAsync();
            return Ok(events);
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchEvents([FromQuery] string? keyword, [FromQuery] int[] categories, [FromQuery] int[] cities)
        {
            IEnumerable<Event> events = await _eventService.SearchEventsAsync(keyword, categories, cities);
            return Ok(events);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEventById(int id)
        {
            Event eventObject = await _eventService.GetEventByIdAsync(id);

            if (eventObject == null)
            {
                return NotFound();
            }

            return Ok(eventObject);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateEvent(int id, [FromBody] UpdateEventDto updateEventDto)
        {
            if (updateEventDto == null)
            {
                return BadRequest("Event data is null.");
            }

            await _eventService.UpdateEventAsync(id, updateEventDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            await _eventService.DeleteEventAsync(id);
            return NoContent();
        }

        [HttpPost("register/{eventId}")]
        [Authorize]
        public async Task<IActionResult> RegisterToEvent(int eventId, SurveyAnswerDto[] answersDto)
        {
            if (answersDto == null)
            {
                return BadRequest("Answers is null or empty.");
            }

            EventSurveyAnswer[] eventSurveyAnswers = _mapper.Map<EventSurveyAnswer[]>(answersDto);

            await _registrationService.Register(eventId, eventSurveyAnswers);
            return NoContent();
        }

        [HttpPost("unregister/{eventId}")]
        [Authorize]
        public async Task<IActionResult> UnregisterFromEvent(int eventId)
        {
            await _registrationService.Unregister(eventId);
            return NoContent();
        }

        [HttpGet("export/{eventId}")]
        [Authorize]
        public async Task<IActionResult> ExportParticipantsAnswers(int eventId)
        {
            try
            {
                MemoryStream? excelFile = await _excelExportService.ExportEventParticipantsAnswers(eventId);
                return File(excelFile, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"учасники-{DateTime.Now.ToString("yyyy-MM-dd HH:mm")}.xlsx");
            }
            catch (Exception ex)
            {
                return BadRequest("Could not export data: " + ex.Message);
            }
        }
    }
}

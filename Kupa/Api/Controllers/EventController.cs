using AutoMapper;
using Kupa.Api.Dtos;
using Kupa.Api.Models;
using Kupa.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Kupa.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IEventService _eventService;
        private readonly IMapper _mapper;

        public EventsController(IEventService eventService, IMapper mapper)
        {
            _eventService = eventService;
            _mapper = mapper;
        }

        [HttpPost]
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
        public async Task<IActionResult> DeleteEvent(int id)
        {
            await _eventService.DeleteEventAsync(id);
            return NoContent();
        }
    }
}

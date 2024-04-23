using AutoMapper;
using Kupa.Api.Dtos;
using Kupa.Api.Models;
using Kupa.Api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kupa.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventCommentController : ControllerBase
    {
        private readonly IEventCommentService _eventCommentService;
        private readonly IMapper _mapper;

        public EventCommentController(IEventCommentService eventCommentService, IMapper mapper) 
        {
            _eventCommentService = eventCommentService;
            _mapper = mapper;
        }

        [HttpPost("event/{id}")]
        [Authorize]
        public async Task<IActionResult> AddNewComment(int eventId, CommentDto commentDto)
        {
            if (commentDto == null || string.IsNullOrEmpty(commentDto.Comment))
            {
                return BadRequest("Comment is null or empty.");
            }

            EventComment eventComment = _mapper.Map<EventComment>(commentDto);
            eventComment.EventId = eventId;
            
            await _eventCommentService.AddNewCommentAsync(eventComment);
            return NoContent();
        }

        [HttpDelete("id")]
        [Authorize]
        public async Task<IActionResult> DeleteComment(int id)
        {
            if (id < 0)
            {
                return BadRequest("Id of comment must be positive number.");
            }

            await _eventCommentService.DeleteCommentAsync(id);
            return NoContent();
        }
    }
}

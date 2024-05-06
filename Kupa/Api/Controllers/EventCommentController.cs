using AutoMapper;
using Kupa.Api.Dtos;
using Kupa.Api.Models;
using Kupa.Api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Kupa.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventCommentController : ControllerBase
    {
        private readonly IEventCommentService eventCommentService;
        private readonly IValidator validator;
        private readonly IMapper mapper;

        public EventCommentController(
            IEventCommentService eventCommentService, 
            IValidator validator,
            IMapper mapper)
        {
            this.eventCommentService = eventCommentService;
            this.validator = validator;
            this.mapper = mapper;
        }

        [HttpPost("event/{eventId}")]
        [Authorize]
        public async Task<IActionResult> AddNewComment(int eventId, CommentDto commentDto)
        {
            validator.PositiveInt(eventId, nameof(eventId));
            validator.ObjectNull(commentDto, nameof(commentDto));
            validator.StringNullOrEmpty(commentDto.Comment, "Comment");

            EventComment eventComment = mapper.Map<EventComment>(commentDto);
            eventComment.EventId = eventId;
            
            await eventCommentService.AddNewCommentAsync(eventComment);
            return NoContent();
        }

        [HttpDelete("id")]
        [Authorize]
        public async Task<IActionResult> DeleteComment(int id)
        {
            validator.PositiveInt(id, nameof(id));

            await eventCommentService.DeleteCommentAsync(id);
            return NoContent();
        }
    }
}

using Kupa.Api.Models;
using Kupa.Api.Repositories.Interfaces;
using Kupa.Api.Services.Interfaces;

namespace Kupa.Api.Services.Implementations
{
    public class EventCommentService : CurrentUserService, IEventCommentService
    {
        private readonly IEventRepository _eventRepository;
        private readonly IEventCommentRepository _eventCommentRepository;

        public EventCommentService(
            IHttpContextAccessor httpContextAccessor, 
            IEventCommentRepository eventCommentRepository,
            IEventRepository eventRepository) : base(httpContextAccessor)
        {
            _eventCommentRepository = eventCommentRepository;
            _eventRepository = eventRepository;
        }

        public async Task AddNewCommentAsync(EventComment comment)
        {
            await CheckIfEventExists(comment.EventId);

            if (UserId == null)
            {
                throw new UnauthorizedAccessException("Not authorized");
            }

            comment.CreatedByUserId = (int)UserId;

            await _eventCommentRepository.AddAsync(comment);
        }

        public async Task DeleteCommentAsync(int id)
        {
            EventComment eventComment = await _eventCommentRepository.GetByIdAsync(id);

            if (eventComment == null)
            {
                throw new ArgumentException($"Comment with id {id} doesn't exist");
            }

            if (eventComment.CreatedByUserId != UserId && UserRole != "Admin")
            {
                throw new UnauthorizedAccessException("You don't have access to delete this comment.");
            }

            await _eventCommentRepository.DeleteAsync(eventComment);
        }

        private async Task CheckIfEventExists(int eventId)
        {
            Event eventObject = await _eventRepository.GetByIdAsync(eventId);

            if (eventObject == null)
            {
                throw new KeyNotFoundException($"Event with id {eventId} not found!");
            }
        }
    }
}

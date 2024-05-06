using Kupa.Api.Models;
using Kupa.Api.Repositories.Interfaces;
using Kupa.Api.Services.Interfaces;
using Microsoft.Extensions.Logging;
using static Microsoft.IO.RecyclableMemoryStreamManager;

namespace Kupa.Api.Services.Implementations
{
    public class EventCommentService : CurrentUserService, IEventCommentService
    {
        private readonly IEventRepository eventRepository;
        private readonly IEventCommentRepository eventCommentRepository;
        private IValidator validator;

        public EventCommentService(
            IHttpContextAccessor httpContextAccessor, 
            IEventCommentRepository eventCommentRepository,
            IEventRepository eventRepository,
            IValidator validator) : base(httpContextAccessor)
        {
            this.eventCommentRepository = eventCommentRepository;
            this.eventRepository = eventRepository;
            this.validator = validator;
        }

        public async Task AddNewCommentAsync(EventComment comment)
        {
            validator.ObjectNull(await eventRepository.GetByIdAsync(comment.EventId), string.Empty, $"Event with id {comment.EventId} not found!");
            validator.AuthorizedUser(UserId);

            comment.CreatedByUserId = (int)UserId;

            await eventCommentRepository.AddAsync(comment);
        }

        public async Task DeleteCommentAsync(int id)
        {
            EventComment eventComment = await eventCommentRepository.GetByIdAsync(id);

            validator.ObjectNull(eventComment, string.Empty, $"Comment with id {id} doesn't exist");
            validator.AuthorizedOrAdminAction(eventComment.CreatedByUserId, (int)UserId, UserRole, "you can't delete this comment");

            await eventCommentRepository.DeleteAsync(eventComment);
        }
    }
}

using Kupa.Api.Data;
using Kupa.Api.Models;
using Kupa.Api.Repositories.Base;
using Kupa.Api.Repositories.Interfaces;

namespace Kupa.Api.Repositories.Implementations
{
    public class EventCommentRepository : RepositoryBase<EventComment>, IEventCommentRepository
    {
        public EventCommentRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<EventComment> GetByIdAsync (int id)
        {
            return await GetByIdAsync(id);
        }

        public async Task AddAsync(EventComment comment)
        {
            comment.CreatedAt = DateTime.Now;
            comment.LastUpdatedAt = DateTime.Now;
            await AddItemAsync(comment);
        }

        public async Task DeleteAsync(int id)
        {
            await DeleteAsync(id);
        }
    }
}

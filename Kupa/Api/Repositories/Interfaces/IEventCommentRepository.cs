using Kupa.Api.Models;

namespace Kupa.Api.Repositories.Interfaces
{
    public interface IEventCommentRepository
    {
        Task<EventComment> GetByIdAsync(int id);

        Task AddAsync(EventComment comment);

        Task DeleteAsync(EventComment comment);
    }
}

using Kupa.Api.Models;

namespace Kupa.Api.Repositories.Interfaces
{
    public interface IEventCommentRepository
    {
        Task<EventComment> GetByIdAsync(int id);

        Task AddAsync(EventComment commentDto);

        Task DeleteAsync(int id);
    }
}

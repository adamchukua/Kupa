using Kupa.Api.Models;

namespace Kupa.Api.Services.Interfaces
{
    public interface IEventCommentService
    {
        Task AddNewCommentAsync(EventComment comment);

        Task DeleteCommentAsync(int id);
    }
}

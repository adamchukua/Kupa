using Kupa.Api.Models;

namespace Kupa.Api.Repositories.Interfaces
{
    public interface IRegistrationRepository
    {
        Task RegisterAsync(EventRegistration registration);

        Task<EventRegistration> GetByEventId(int eventId, int userId);

        Task DeleteAsync(EventRegistration registration);
    }
}

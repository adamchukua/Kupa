using Kupa.Api.Data;
using Kupa.Api.Models;
using Kupa.Api.Repositories.Base;
using Kupa.Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Kupa.Api.Repositories.Implementations
{
    public class RegistrationRepository : RepositoryBase<EventRegistration>, IRegistrationRepository
    {
        public RegistrationRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task DeleteAsync(EventRegistration registration)
        {
            await DeleteItemAsync(registration);
        }

        public async Task<EventRegistration> GetByEventId(int eventId, int userId)
        {
            return await Where(r => r.EventId == eventId && r.UserId == userId)
                .FirstOrDefaultAsync();
        }

        public async Task RegisterAsync(EventRegistration registration)
        {
            registration.CreatedAt = DateTime.Now;
            await AddItemAsync(registration);
        }
    }
}

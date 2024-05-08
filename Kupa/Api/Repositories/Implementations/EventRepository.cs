using Kupa.Api.Data;
using Kupa.Api.Models;
using Kupa.Api.Repositories.Base;
using Kupa.Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Kupa.Api.Repositories.Implementations
{
    public class EventRepository : RepositoryBase<Event>, IEventRepository
    {
        private readonly IRegistrationRepository _registrationRepository;

        public EventRepository(ApplicationDbContext context, IRegistrationRepository registrationRepository) : base(context)
        {
            _registrationRepository = registrationRepository;
        }

        public async Task<Event> GetByIdAsync(int id)
        {
            return await Where(e => e.Id == id)
                .Include(e => e.Category)
                .Include(e => e.Status)
                .Include(e => e.Location)
                    .ThenInclude(e => e.City)
                .Include(e => e.User)
                    .ThenInclude(e => e.Profile)
                .Include(e => e.EventComments)
                    .ThenInclude(e => e.User)
                        .ThenInclude(e => e.Profile)
                .Include(e => e.EventRegistrations)
                    .ThenInclude(e => e.EventSurveyAnswers)
                .Include(e => e.EventRegistrations)
                    .ThenInclude(e => e.User)
                        .ThenInclude(e => e.Profile)
                .Include(e => e.EventSurveyQuestions)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Event>> GetAsync(int userId, bool createdByUser, bool participatedByUser)
        {
            IQueryable<Event> query = null;

            if (createdByUser)
            {
                query = Where(e => e.CreatedByUserId == userId);
            }

            if (participatedByUser)
            {
                if (query == null)
                {
                    query = Where(e => e.EventRegistrations.Any(r => r.UserId == userId));
                }

                query = query.Where(e => e.EventRegistrations.Any(r => r.UserId == userId));
            }

            if (!createdByUser && !participatedByUser)
            {
                return await GetAllItemsAsync();
            }
            else
            {
                return await query.ToListAsync();
            }
        }

        public async Task<IEnumerable<Event>> SearchAsync(string? keyword, int[] categories, int[] cities)
        {
            IQueryable<Event> query = null;

            if (!string.IsNullOrEmpty(keyword))
            {
                query = Where(e => e.Title.Contains(keyword) || (e.Description != null && e.Description.Contains(keyword)));
            }

            if (categories.Length > 0)
            {
                if (query == null)
                {
                    query = Where(e => categories.Contains(e.CategoryId));
                }

                query = query.Where(e => categories.Contains(e.CategoryId));
            }

            if (cities.Length > 0)
            {
                if (query == null)
                {
                    query = Where(e => cities.Contains(e.CityId));
                }

                query = query.Where(e => cities.Contains(e.CityId));
            }

            if (query == null)
            {
                return await GetAllItemsAsync();
            }

            return await query.ToListAsync();
        }

        public async Task AddAsync(Event eventObject)
        {
            DateTime now = DateTime.Now;
            eventObject.CreatedAt = now;
            eventObject.LastUpdatedAt = now;

            if (eventObject.EventSurveyQuestions != null)
            {
                foreach (var question in eventObject.EventSurveyQuestions)
                {
                    question.CreatedAt = now;
                    question.LastUpdatedAt = now;
                }
            }

            await AddItemAsync(eventObject);
        }

        public async Task UpdateAsync(Event eventObject)
        {
            DateTime now = DateTime.Now;
            eventObject.LastUpdatedAt = now;

            if (eventObject.EventSurveyQuestions != null)
            {
                foreach (var question in eventObject.EventSurveyQuestions)
                {
                    question.LastUpdatedAt = now;
                }
            }

            await UpdateItemAsync(eventObject);
        }

        public async Task DeleteAsync(Event eventObject)
        {
            await DeleteItemAsync(eventObject);
        }

        public async Task<bool> ExistsByIdAsync(int id)
        {
            return await ExistsAsync(e => e.Id == id);
        }

        public async Task<bool> ExistsByLocationIdAsync(int locationId)
        {
            return await ExistsAsync(e => e.LocationId == locationId);
        }

        public async Task<int> CountEventsByLocationIdAsync(int locationId)
        {
            return await Where(e =>e.LocationId == locationId)
                .CountAsync();
        }
    }
}

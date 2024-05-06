using Kupa.Api.Data;
using Kupa.Api.Models;
using Kupa.Api.Repositories.Base;
using Kupa.Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Kupa.Api.Repositories.Implementations
{
    public class EventRepository : RepositoryBase<Event>, IEventRepository
    {
        public EventRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Event> GetByIdAsync(int id)
        {
            return await Where(e => e.Id == id)
                .Include(e => e.Status)
                .Include(e => e.Location)
                .Include(e => e.User)
                .Include(e => e.EventComments)
                .Include(e => e.EventRegistrations)
                    .ThenInclude(e => e.EventSurveyAnswers)
                .Include(e => e.EventRegistrations)
                    .ThenInclude(e => e.User)
                        .ThenInclude(e => e.Profile)
                .Include(e => e.EventSurveyQuestions)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Event>> GetAllAsync()
        {
            return await GetAllItemsAsync();
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

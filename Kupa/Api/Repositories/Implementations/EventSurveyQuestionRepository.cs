using Kupa.Api.Data;
using Kupa.Api.Models;
using Kupa.Api.Repositories.Base;
using Kupa.Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Kupa.Api.Repositories.Implementations
{
    public class EventSurveyQuestionRepository : RepositoryBase<EventSurveyQuestion>, IEventSurveyQuestionRepository
    {
        public EventSurveyQuestionRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<EventSurveyQuestion>> GetSurveyQuestionsByEventId(int eventId)
        {
            return await Where(e => e.EventId == eventId)
                .ToListAsync();
        }

        public async Task DeleteRange(IEnumerable<EventSurveyQuestion> surveyQuestions)
        {
            await DeleteRangeAsync(surveyQuestions);
        }
    }
}

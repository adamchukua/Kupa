using Kupa.Api.Data;
using Kupa.Api.Models;
using Kupa.Api.Repositories.Base;
using Kupa.Api.Repositories.Interfaces;

namespace Kupa.Api.Repositories.Implementations
{
    public class EventSurveyAnswerRepository : RepositoryBase<EventSurveyAnswer>, IEventSurveyAnswerRepository
    {
        public EventSurveyAnswerRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task AddAnswers(EventSurveyAnswer[] answers)
        {
            foreach (EventSurveyAnswer answer in answers)
            {
                answer.CreatedAt = DateTime.Now;
                answer.LastUpdatedAt = DateTime.Now;
            }

            await AddRangeAsync(answers);
        }
    }
}

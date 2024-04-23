using Kupa.Api.Models;

namespace Kupa.Api.Repositories.Interfaces
{
    public interface IEventSurveyQuestionRepository
    {
        public Task<IEnumerable<EventSurveyQuestion>> GetSurveyQuestionsByEventId(int eventId);

        public Task DeleteRange(IEnumerable<EventSurveyQuestion> surveyQuestions);
    }
}

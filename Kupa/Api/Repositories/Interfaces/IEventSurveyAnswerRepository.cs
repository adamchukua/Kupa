using Kupa.Api.Models;

namespace Kupa.Api.Repositories.Interfaces
{
    public interface IEventSurveyAnswerRepository
    {
        public Task AddAnswers(EventSurveyAnswer[] answers);
    }
}

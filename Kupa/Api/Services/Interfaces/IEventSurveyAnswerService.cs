using Kupa.Api.Models;

namespace Kupa.Api.Services.Interfaces
{
    public interface IEventSurveyAnswerService
    {
        public Task AddAnswersAsync(int eventId, EventSurveyAnswer[] answers);
    }
}

using Kupa.Api.Models;

namespace Kupa.Api.Services.Interfaces
{
    public interface IRegistrationService
    {
        Task Register(int eventId, EventSurveyAnswer[] eventSurveyAnswers);

        Task Unregister(int eventId);
    }
}

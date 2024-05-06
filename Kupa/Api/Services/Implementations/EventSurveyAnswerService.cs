using Kupa.Api.Models;
using Kupa.Api.Repositories.Interfaces;
using Kupa.Api.Services.Interfaces;

namespace Kupa.Api.Services.Implementations
{
    public class EventSurveyAnswerService : CurrentUserService, IEventSurveyAnswerService
    {
        private readonly IEventSurveyAnswerRepository eventSurveyAnswerRepository;
        private readonly IEventSurveyQuestionRepository eventSurveyQuestionRepository;
        private readonly IValidator validator;

        public EventSurveyAnswerService(
            IHttpContextAccessor httpContextAccessor, 
            IEventSurveyAnswerRepository eventSurveyAnswerRepository,
            IEventSurveyQuestionRepository eventSurveyQuestionRepository,
            IValidator validator) : base(httpContextAccessor)
        {
            this.eventSurveyAnswerRepository = eventSurveyAnswerRepository;
            this.eventSurveyQuestionRepository = eventSurveyQuestionRepository;
            this.validator = validator;
        }

        public async Task AddAnswersAsync(int eventId, EventSurveyAnswer[] answers)
        {
            validator.AuthorizedUser(UserId);

            IEnumerable<EventSurveyQuestion> eventSurveyQuestions =
                await eventSurveyQuestionRepository.GetSurveyQuestionsByEventId(eventId);
            IEnumerable<int> requiredQuestionIds = eventSurveyQuestions
                .Where(e => e.IsRequired)
                .Select(q => q.Id);

            HashSet<int> answeredQuestionIds = new HashSet<int>(answers.Select(a => a.EventSurveyQuestionId));

            if (!requiredQuestionIds.All(id => answeredQuestionIds.Contains(id)))
            {
                throw new ArgumentException("Not all required questions have been answered.");
            }

            foreach (EventSurveyAnswer answer in answers)
            {
                answer.CreatedByUserId = (int)UserId;
            }

            await eventSurveyAnswerRepository.AddAnswers(answers);
        }
    }
}

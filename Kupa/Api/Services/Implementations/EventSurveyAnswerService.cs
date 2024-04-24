using Kupa.Api.Models;
using Kupa.Api.Repositories.Interfaces;
using Kupa.Api.Services.Interfaces;

namespace Kupa.Api.Services.Implementations
{
    public class EventSurveyAnswerService : UserService, IEventSurveyAnswerService
    {
        private readonly IEventSurveyAnswerRepository _eventSurveyAnswerRepository;
        private readonly IEventSurveyQuestionRepository _eventSurveyQuestionRepository;

        public EventSurveyAnswerService(
            IHttpContextAccessor httpContextAccessor, 
            IEventSurveyAnswerRepository eventSurveyAnswerRepository,
            IEventSurveyQuestionRepository eventSurveyQuestionRepository) : base(httpContextAccessor)
        {
            _eventSurveyAnswerRepository = eventSurveyAnswerRepository;
            _eventSurveyQuestionRepository = eventSurveyQuestionRepository;
        }

        public async Task AddAnswersAsync(int eventId, EventSurveyAnswer[] answers)
        {
            if (UserId == null)
            {
                throw new UnauthorizedAccessException("Not authorized");
            };

            IEnumerable<EventSurveyQuestion> eventSurveyQuestions =
                await _eventSurveyQuestionRepository.GetSurveyQuestionsByEventId(eventId);
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

            await _eventSurveyAnswerRepository.AddAnswers(answers);
        }
    }
}

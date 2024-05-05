using Kupa.Api.Data;
using Kupa.Api.Models;
using Kupa.Api.Repositories.Interfaces;
using Kupa.Api.Services.Interfaces;

namespace Kupa.Api.Services.Implementations
{
    public class RegistrationService : UserIdService, IRegistrationService
    {
        private ApplicationDbContext _context;
        private readonly IEventSurveyAnswerService _eventSurveyAnswerService;
        private readonly IEventRepository _eventRepository;
        private readonly IRegistrationRepository _registrationRepository;
        private readonly IEventSurveyAnswerRepository _eventSurveyAnswerRepository;
        private readonly IUserRepository _userRepository;

        public RegistrationService(
            ApplicationDbContext context,
            IHttpContextAccessor httpContextAccessor, 
            IRegistrationRepository registrationRepository,
            IEventRepository eventRepository,
            IEventSurveyAnswerService eventSurveyAnswerService,
            IUserRepository userRepository) : base(httpContextAccessor)
        {
            _context = context;
            _registrationRepository = registrationRepository;
            _eventRepository = eventRepository;
            _eventSurveyAnswerService = eventSurveyAnswerService;
            _userRepository = userRepository;
        }

        public async Task Register(int eventId, EventSurveyAnswer[] eventSurveyAnswers)
        {
            if (UserId == null)
            {
                throw new UnauthorizedAccessException("To register to event you need authorize first");
            }

            User user = await _userRepository.GetByIdAsync((int)UserId);

            if (IsProfileCompleted(user.Profile))
            {
                throw new Exception("Your profile isn't fully completed");
            }

            bool eventExists = await _eventRepository.ExistsByIdAsync(eventId);

            if (!eventExists)
            {
                throw new ArgumentException($"Event with id {eventId} doesn't exist");
            }

            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    EventRegistration registration = EventRegistration.CreateRegistration(eventId, (int)UserId);

                    await _registrationRepository.RegisterAsync(registration);

                    foreach (var eventSurveyAnswer in eventSurveyAnswers)
                    {
                        eventSurveyAnswer.RegistrationId = registration.Id;
                    }

                    await _eventSurveyAnswerService.AddAnswersAsync(eventId, eventSurveyAnswers);
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public async Task Unregister(int eventId)
        {
            if (UserId == null)
            {
                throw new UnauthorizedAccessException("To register to event you need authorize first");
            }

            bool eventExists = await _eventRepository.ExistsByIdAsync(eventId);

            if (!eventExists)
            {
                throw new ArgumentException($"Event with id {eventId} doesn't exist");
            }

            EventRegistration eventRegistration = await _registrationRepository.GetByEventId(eventId, (int)UserId);
           
            await _registrationRepository.DeleteAsync(eventRegistration);
        }

        private bool IsProfileCompleted(UserProfile profile)
        {
            return !string.IsNullOrEmpty(profile.Name) &&
                !string.IsNullOrEmpty(profile.PhoneNumber) &&
                !string.IsNullOrEmpty(profile.Activity) &&
                !string.IsNullOrEmpty(profile.TelegramUsername);
        }
    }
}

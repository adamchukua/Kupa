using Kupa.Api.Data;
using Kupa.Api.Models;
using Kupa.Api.Repositories.Interfaces;
using Kupa.Api.Services.Interfaces;

namespace Kupa.Api.Services.Implementations
{
    public class RegistrationService : CurrentUserService, IRegistrationService
    {
        private ApplicationDbContext context;
        private readonly IEventSurveyAnswerService eventSurveyAnswerService;
        private readonly IEventRepository eventRepository;
        private readonly IRegistrationRepository registrationRepository;
        private readonly IUserRepository userRepository;
        private readonly IValidator validator;

        public RegistrationService(
            ApplicationDbContext context,
            IHttpContextAccessor httpContextAccessor, 
            IRegistrationRepository registrationRepository,
            IEventRepository eventRepository,
            IEventSurveyAnswerService eventSurveyAnswerService,
            IUserRepository userRepository,
            IValidator validator) : base(httpContextAccessor)
        {
            this.context = context;
            this.registrationRepository = registrationRepository;
            this.eventRepository = eventRepository;
            this.eventSurveyAnswerService = eventSurveyAnswerService;
            this.userRepository = userRepository;
            this.validator = validator;
        }

        public async Task Register(int eventId, EventSurveyAnswer[] eventSurveyAnswers)
        {
            validator.AuthorizedUser(UserId);

            User user = await userRepository.GetByIdAsync((int)UserId);

            if (IsProfileCompleted(user.Profile))
            {
                throw new Exception("Your profile isn't fully completed");
            }

            bool eventExists = await eventRepository.ExistsByIdAsync(eventId);

            if (!eventExists)
            {
                throw new ArgumentException($"Event with id {eventId} doesn't exist");
            }

            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    EventRegistration registration = EventRegistration.CreateRegistration(eventId, (int)UserId);

                    await registrationRepository.RegisterAsync(registration);

                    foreach (var eventSurveyAnswer in eventSurveyAnswers)
                    {
                        eventSurveyAnswer.RegistrationId = registration.Id;
                    }

                    await eventSurveyAnswerService.AddAnswersAsync(eventId, eventSurveyAnswers);
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
            validator.AuthorizedUser(UserId);

            bool eventExists = await eventRepository.ExistsByIdAsync(eventId);

            if (!eventExists)
            {
                throw new ArgumentException($"Event with id {eventId} doesn't exist");
            }

            EventRegistration eventRegistration = await registrationRepository.GetByEventId(eventId, (int)UserId);
           
            await registrationRepository.DeleteAsync(eventRegistration);
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

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

        public RegistrationService(
            ApplicationDbContext context,
            IHttpContextAccessor httpContextAccessor, 
            IRegistrationRepository registrationRepository,
            IEventRepository eventRepository,
            IEventSurveyAnswerService eventSurveyAnswerService) : base(httpContextAccessor)
        {
            _context = context;
            _registrationRepository = registrationRepository;
            _eventRepository = eventRepository;
            _eventSurveyAnswerService = eventSurveyAnswerService;
        }

        public async Task Register(int eventId, EventSurveyAnswer[] eventSurveyAnswers)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
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

            //using (var transaction = _context.Database.BeginTransaction())
            //{
            //    try
            //    {
            //        _eventSurveyAnswerRepository.DeleteAnswersAsync(eventRegistration.EventSurveyAnswers);
            //        _even

            //        transaction.Commit();
            //    }
            //    catch (Exception)
            //    {
            //        transaction.Rollback();
            //        throw;
            //    }
            //}
        }
    }
}

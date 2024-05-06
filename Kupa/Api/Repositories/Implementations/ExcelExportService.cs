using Kupa.Api.Models;
using Kupa.Api.Repositories.Interfaces;
using OfficeOpenXml;

namespace Kupa.Api.Repositories.Implementations
{
    public class ExcelExportService : IExcelExportService
    {
        private readonly IEventRepository _eventRepository;

        public ExcelExportService(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        }

        public async Task<MemoryStream> ExportEventParticipantsAnswers(int eventId)
        {
            Event eventInfo = await _eventRepository.GetByIdAsync(eventId);
            MemoryStream stream = new MemoryStream();

            using (var package = new ExcelPackage(stream))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Учасники");

                worksheet.Cells[1, 1].Value = "Ім'я";
                worksheet.Cells[1, 2].Value = "Рік народження";
                worksheet.Cells[1, 3].Value = "Номер телефону";
                worksheet.Cells[1, 4].Value = "Сфера";
                worksheet.Cells[1, 5].Value = "Телеграм";

                int questionRowId = 6;
                foreach (EventSurveyQuestion question in eventInfo.EventSurveyQuestions)
                {
                    worksheet.Cells[1, questionRowId].Value = question.Question;

                    questionRowId++;
                }

                int row = 2;
                foreach (EventRegistration registration in eventInfo.EventRegistrations)
                {
                    worksheet.Cells[row, 1].Value = registration.User.Profile.Name;
                    worksheet.Cells[row, 2].Value = registration.User.Profile.BirthYear;
                    worksheet.Cells[row, 3].Value = registration.User.Profile.PhoneNumber;
                    worksheet.Cells[row, 4].Value = registration.User.Profile.Activity;
                    worksheet.Cells[row, 5].Value = registration.User.Profile.TelegramUsername;

                    questionRowId = 6;
                    foreach (EventSurveyAnswer answer in registration.EventSurveyAnswers)
                    {
                        worksheet.Cells[row, questionRowId].Value = answer.Answer;
                    }
                }

                package.Save();
            }

            stream.Position = 0;
            return stream;
        }
    }
}

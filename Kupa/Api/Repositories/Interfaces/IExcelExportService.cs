namespace Kupa.Api.Repositories.Interfaces
{
    public interface IExcelExportService
    {
        Task<MemoryStream> ExportEventParticipantsAnswers(int eventId);
    }
}

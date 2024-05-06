using Kupa.Api.Enums;

namespace Kupa.Api.Dtos
{
    public class NewEventDto
    {
        public string Title { get; set; }

        public string? Description { get; set; }

        public LocationTypeId LocationTypeId { get; set; }

        public string Location {  get; set; }

        public int CategoryId { get; set; }

        public int CityId { get; set; }

        public List<SurveyQuestionDto> SurveyQuestions { get; set; }
    }
}

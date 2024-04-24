using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kupa.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueIndexEventSurveyAnswer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_EventSurveyAnswers_EventSurveyQuestionId",
                table: "EventSurveyAnswers");

            migrationBuilder.CreateIndex(
                name: "IX_EventSurveyAnswers_EventSurveyQuestionId_CreatedByUserId",
                table: "EventSurveyAnswers",
                columns: new[] { "EventSurveyQuestionId", "CreatedByUserId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_EventSurveyAnswers_EventSurveyQuestionId_CreatedByUserId",
                table: "EventSurveyAnswers");

            migrationBuilder.CreateIndex(
                name: "IX_EventSurveyAnswers_EventSurveyQuestionId",
                table: "EventSurveyAnswers",
                column: "EventSurveyQuestionId");
        }
    }
}

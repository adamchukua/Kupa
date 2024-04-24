using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kupa.Migrations
{
    /// <inheritdoc />
    public partial class AddCreatedByUserIdEventSurveyAnswers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventSurveyAnswers_AspNetUsers_UserId",
                table: "EventSurveyAnswers");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "EventSurveyAnswers",
                newName: "CreatedByUserId");

            migrationBuilder.RenameIndex(
                name: "IX_EventSurveyAnswers_UserId",
                table: "EventSurveyAnswers",
                newName: "IX_EventSurveyAnswers_CreatedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_EventSurveyAnswers_AspNetUsers_CreatedByUserId",
                table: "EventSurveyAnswers",
                column: "CreatedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventSurveyAnswers_AspNetUsers_CreatedByUserId",
                table: "EventSurveyAnswers");

            migrationBuilder.RenameColumn(
                name: "CreatedByUserId",
                table: "EventSurveyAnswers",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_EventSurveyAnswers_CreatedByUserId",
                table: "EventSurveyAnswers",
                newName: "IX_EventSurveyAnswers_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_EventSurveyAnswers_AspNetUsers_UserId",
                table: "EventSurveyAnswers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

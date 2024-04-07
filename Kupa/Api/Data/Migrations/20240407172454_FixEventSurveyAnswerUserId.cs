using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kupa.Migrations
{
    /// <inheritdoc />
    public partial class FixEventSurveyAnswerUserId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventSurveyAnswers_AspNetUsers_UserId1",
                table: "EventSurveyAnswers");

            migrationBuilder.DropIndex(
                name: "IX_EventSurveyAnswers_UserId1",
                table: "EventSurveyAnswers");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "EventSurveyAnswers");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "EventSurveyAnswers",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_EventSurveyAnswers_UserId",
                table: "EventSurveyAnswers",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_EventSurveyAnswers_AspNetUsers_UserId",
                table: "EventSurveyAnswers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventSurveyAnswers_AspNetUsers_UserId",
                table: "EventSurveyAnswers");

            migrationBuilder.DropIndex(
                name: "IX_EventSurveyAnswers_UserId",
                table: "EventSurveyAnswers");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "EventSurveyAnswers",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "EventSurveyAnswers",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_EventSurveyAnswers_UserId1",
                table: "EventSurveyAnswers",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_EventSurveyAnswers_AspNetUsers_UserId1",
                table: "EventSurveyAnswers",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

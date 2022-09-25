using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZaminEducation.Data.Migrations
{
    public partial class CourseVideoMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuizResults_Users_UserId",
                table: "QuizResults");

            migrationBuilder.AddForeignKey(
                name: "FK_QuizResults_Users_UserId",
                table: "QuizResults",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuizResults_Users_UserId",
                table: "QuizResults");

            migrationBuilder.AddForeignKey(
                name: "FK_QuizResults_Users_UserId",
                table: "QuizResults",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}

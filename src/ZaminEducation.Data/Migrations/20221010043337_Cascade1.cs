using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZaminEducation.Data.Migrations
{
    public partial class Cascade1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Attachments_ImageId",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseVideos_Courses_CourseId",
                table: "CourseVideos");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "CourseVideos",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CourseId1",
                table: "CourseVideos",
                type: "bigint",
                nullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "ImageId",
                table: "Courses",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.CreateIndex(
                name: "IX_CourseVideos_CourseId1",
                table: "CourseVideos",
                column: "CourseId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Attachments_ImageId",
                table: "Courses",
                column: "ImageId",
                principalTable: "Attachments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseVideos_Courses_CourseId",
                table: "CourseVideos",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseVideos_Courses_CourseId1",
                table: "CourseVideos",
                column: "CourseId1",
                principalTable: "Courses",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Attachments_ImageId",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseVideos_Courses_CourseId",
                table: "CourseVideos");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseVideos_Courses_CourseId1",
                table: "CourseVideos");

            migrationBuilder.DropIndex(
                name: "IX_CourseVideos_CourseId1",
                table: "CourseVideos");

            migrationBuilder.DropColumn(
                name: "CourseId1",
                table: "CourseVideos");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "CourseVideos",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "ImageId",
                table: "Courses",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Attachments_ImageId",
                table: "Courses",
                column: "ImageId",
                principalTable: "Attachments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseVideos_Courses_CourseId",
                table: "CourseVideos",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id");
        }
    }
}

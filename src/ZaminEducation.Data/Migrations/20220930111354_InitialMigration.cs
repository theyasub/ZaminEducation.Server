using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZaminEducation.Data.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HashTags_Courses_CourseId",
                table: "HashTags");

            migrationBuilder.AlterColumn<long>(
                name: "CourseId",
                table: "HashTags",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "YouTubePlaylistLink",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_HashTags_Courses_CourseId",
                table: "HashTags",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HashTags_Courses_CourseId",
                table: "HashTags");

            migrationBuilder.DropColumn(
                name: "YouTubePlaylistLink",
                table: "Courses");

            migrationBuilder.AlterColumn<long>(
                name: "CourseId",
                table: "HashTags",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_HashTags_Courses_CourseId",
                table: "HashTags",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZaminEducation.Data.Migrations
{
    public partial class youTubeVideo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseVideos_Courses_CourseId",
                table: "CourseVideos");

            migrationBuilder.DropTable(
                name: "YouTubeVideos");

            migrationBuilder.RenameColumn(
                name: "Link",
                table: "CourseVideos",
                newName: "Url");

            migrationBuilder.AlterColumn<long>(
                name: "CourseId",
                table: "CourseVideos",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<string>(
                name: "Thumbnail",
                table: "CourseVideos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseVideos_Courses_CourseId",
                table: "CourseVideos",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseVideos_Courses_CourseId",
                table: "CourseVideos");

            migrationBuilder.DropColumn(
                name: "Thumbnail",
                table: "CourseVideos");

            migrationBuilder.RenameColumn(
                name: "Url",
                table: "CourseVideos",
                newName: "Link");

            migrationBuilder.AlterColumn<long>(
                name: "CourseId",
                table: "CourseVideos",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "YouTubeVideos",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Length = table.Column<long>(type: "bigint", nullable: false),
                    State = table.Column<int>(type: "int", nullable: false),
                    Thumbnail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_YouTubeVideos", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_CourseVideos_Courses_CourseId",
                table: "CourseVideos",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

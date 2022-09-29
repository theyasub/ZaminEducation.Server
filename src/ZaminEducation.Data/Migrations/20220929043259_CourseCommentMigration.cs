using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZaminEducation.Data.Migrations
{
    public partial class CourseCommentMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsReplied",
                table: "CourseComments",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<long>(
                name: "ImageId",
                table: "Certificates",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Certificates_ImageId",
                table: "Certificates",
                column: "ImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Certificates_Attachments_ImageId",
                table: "Certificates",
                column: "ImageId",
                principalTable: "Attachments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Certificates_Attachments_ImageId",
                table: "Certificates");

            migrationBuilder.DropIndex(
                name: "IX_Certificates_ImageId",
                table: "Certificates");

            migrationBuilder.DropColumn(
                name: "IsReplied",
                table: "CourseComments");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "Certificates");
        }
    }
}

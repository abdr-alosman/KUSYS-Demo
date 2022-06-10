using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KUSYS_Demo.Data.Migrations
{
    public partial class editStudentNo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TcPassportNo",
                table: "StudentUsers");

            migrationBuilder.AddColumn<long>(
                name: "StudentNo",
                table: "StudentUsers",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StudentNo",
                table: "StudentUsers");

            migrationBuilder.AddColumn<string>(
                name: "TcPassportNo",
                table: "StudentUsers",
                type: "nvarchar(25)",
                maxLength: 25,
                nullable: false,
                defaultValue: "");
        }
    }
}

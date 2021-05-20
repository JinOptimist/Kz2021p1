using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication1.Migrations
{
    public partial class UpdatePupilAndAddAvatarColumnToPerson : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Subject",
                table: "Pupils");

            migrationBuilder.AddColumn<string>(
                name: "Avatar",
                table: "Students",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Avatar",
                table: "Pupils",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Avatar",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "Avatar",
                table: "Pupils");

            migrationBuilder.AddColumn<string>(
                name: "Subject",
                table: "Pupils",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);
        }
    }
}

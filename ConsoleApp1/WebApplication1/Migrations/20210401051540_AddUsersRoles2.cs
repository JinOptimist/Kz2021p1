using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication1.Migrations
{
    public partial class AddUsersRoles2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "RolesResto",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1L, "admin" });

            migrationBuilder.InsertData(
                table: "RolesResto",
                columns: new[] { "Id", "Name" },
                values: new object[] { 2L, "user" });

            migrationBuilder.InsertData(
                table: "UsersResto",
                columns: new[] { "Id", "Email", "Password", "RoleId" },
                values: new object[] { 1L, "admin@mail.ru", "123456", 1L });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RolesResto",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "UsersResto",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "RolesResto",
                keyColumn: "Id",
                keyValue: 1L);
        }
    }
}

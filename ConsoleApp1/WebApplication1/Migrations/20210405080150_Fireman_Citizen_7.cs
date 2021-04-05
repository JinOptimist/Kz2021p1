using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication1.Migrations
{
    public partial class Fireman_Citizen_7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Fireman",
                table: "Fireman");

            migrationBuilder.DropIndex(
                name: "IX_Fireman_CitizenId_",
                table: "Fireman");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "Fireman",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Fireman",
                table: "Fireman",
                column: "CitizenId_");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Fireman",
                table: "Fireman");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "Fireman",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Fireman",
                table: "Fireman",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Fireman_CitizenId_",
                table: "Fireman",
                column: "CitizenId_",
                unique: true);
        }
    }
}

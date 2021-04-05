using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication1.Migrations
{
    public partial class Fireman_Citizen_8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fireman_Citizens_CitizenId_",
                table: "Fireman");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Fireman",
                table: "Fireman");

            migrationBuilder.DropColumn(
                name: "CitizenId_",
                table: "Fireman");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Fireman",
                table: "Fireman",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Fireman_Citizens_Id",
                table: "Fireman",
                column: "Id",
                principalTable: "Citizens",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fireman_Citizens_Id",
                table: "Fireman");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Fireman",
                table: "Fireman");

            migrationBuilder.AddColumn<long>(
                name: "CitizenId_",
                table: "Fireman",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Fireman",
                table: "Fireman",
                column: "CitizenId_");

            migrationBuilder.AddForeignKey(
                name: "FK_Fireman_Citizens_CitizenId_",
                table: "Fireman",
                column: "CitizenId_",
                principalTable: "Citizens",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

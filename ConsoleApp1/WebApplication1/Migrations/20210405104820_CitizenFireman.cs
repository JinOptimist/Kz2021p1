using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication1.Migrations
{
    public partial class CitizenFireman : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fireman_Citizens_CitizenId_",
                table: "Fireman");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Fireman",
                table: "Fireman");

            migrationBuilder.RenameTable(
                name: "Fireman",
                newName: "Firemen");

            migrationBuilder.RenameColumn(
                name: "CitizenId_",
                table: "Firemen",
                newName: "CitizenId");

            migrationBuilder.RenameIndex(
                name: "IX_Fireman_CitizenId_",
                table: "Firemen",
                newName: "IX_Firemen_CitizenId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Firemen",
                table: "Firemen",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Firemen_Citizens_CitizenId",
                table: "Firemen",
                column: "CitizenId",
                principalTable: "Citizens",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Firemen_Citizens_CitizenId",
                table: "Firemen");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Firemen",
                table: "Firemen");

            migrationBuilder.RenameTable(
                name: "Firemen",
                newName: "Fireman");

            migrationBuilder.RenameColumn(
                name: "CitizenId",
                table: "Fireman",
                newName: "CitizenId_");

            migrationBuilder.RenameIndex(
                name: "IX_Firemen_CitizenId",
                table: "Fireman",
                newName: "IX_Fireman_CitizenId_");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Fireman",
                table: "Fireman",
                column: "Id");

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

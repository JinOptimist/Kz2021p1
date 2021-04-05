using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication1.Migrations
{
    public partial class FiremanCitizen : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CitizenId",
                table: "Fireman",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Fireman_CitizenId",
                table: "Fireman",
                column: "CitizenId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Fireman_Citizens_CitizenId",
                table: "Fireman",
                column: "CitizenId",
                principalTable: "Citizens",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fireman_Citizens_CitizenId",
                table: "Fireman");

            migrationBuilder.DropIndex(
                name: "IX_Fireman_CitizenId",
                table: "Fireman");

            migrationBuilder.DropColumn(
                name: "CitizenId",
                table: "Fireman");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication1.Migrations
{
    public partial class FireTruckTeam : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "FiremanTeamId",
                table: "Firemen",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "FireTrucks",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TruckNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TruckState = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FireTrucks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FiremanTeams",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TeamName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TruckId = table.Column<long>(type: "bigint", nullable: false),
                    Shift = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TeamState = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FiremanTeams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FiremanTeams_FireTrucks_TruckId",
                        column: x => x.TruckId,
                        principalTable: "FireTrucks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Firemen_FiremanTeamId",
                table: "Firemen",
                column: "FiremanTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_FiremanTeams_TruckId",
                table: "FiremanTeams",
                column: "TruckId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Firemen_FiremanTeams_FiremanTeamId",
                table: "Firemen",
                column: "FiremanTeamId",
                principalTable: "FiremanTeams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Firemen_FiremanTeams_FiremanTeamId",
                table: "Firemen");

            migrationBuilder.DropTable(
                name: "FiremanTeams");

            migrationBuilder.DropTable(
                name: "FireTrucks");

            migrationBuilder.DropIndex(
                name: "IX_Firemen_FiremanTeamId",
                table: "Firemen");

            migrationBuilder.DropColumn(
                name: "FiremanTeamId",
                table: "Firemen");
        }
    }
}

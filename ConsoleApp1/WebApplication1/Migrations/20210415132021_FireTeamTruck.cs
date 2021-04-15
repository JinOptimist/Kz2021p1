using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication1.Migrations
{
    public partial class FireTeamTruck : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Police");

            migrationBuilder.AddColumn<long>(
                name: "TeamId",
                table: "Firemen",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
            

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
                name: "IX_Firemen_TeamId",
                table: "Firemen",
                column: "TeamId");
           

            migrationBuilder.CreateIndex(
                name: "IX_FiremanTeams_TruckId",
                table: "FiremanTeams",
                column: "TruckId",
                unique: true);

           
            migrationBuilder.AddForeignKey(
                name: "FK_Firemen_FiremanTeams_TeamId",
                table: "Firemen",
                column: "TeamId",
                principalTable: "FiremanTeams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Firemen_FiremanTeams_TeamId",
                table: "Firemen");

            migrationBuilder.DropTable(
                name: "Buses");

            migrationBuilder.DropTable(
                name: "CertificatePupil");

            migrationBuilder.DropTable(
                name: "CertificateStudent");

            migrationBuilder.DropTable(
                name: "DepartingFlightsInfo");

            migrationBuilder.DropTable(
                name: "FiremanTeams");

            migrationBuilder.DropTable(
                name: "IncomingFlightsInfo");

            migrationBuilder.DropTable(
                name: "Passengers");

            migrationBuilder.DropTable(
                name: "PoliceAcademy",
                schema: "Police");

            migrationBuilder.DropTable(
                name: "PoliceCallHistory",
                schema: "Police");

            migrationBuilder.DropTable(
                name: "SportEvent");

            migrationBuilder.DropTable(
                name: "SportSection");

            migrationBuilder.DropTable(
                name: "Violations",
                schema: "Police");

            migrationBuilder.DropTable(
                name: "TripRoute");

            migrationBuilder.DropTable(
                name: "Pupils");

            migrationBuilder.DropTable(
                name: "Certificates");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "FireTrucks");

            migrationBuilder.DropTable(
                name: "SportComplex");

            migrationBuilder.DropTable(
                name: "Policemen",
                schema: "Police");

            migrationBuilder.DropTable(
                name: "Schools");

            migrationBuilder.DropTable(
                name: "Universities");

            migrationBuilder.DropIndex(
                name: "IX_Firemen_TeamId",
                table: "Firemen");

            migrationBuilder.DropColumn(
                name: "TeamId",
                table: "Firemen");

            migrationBuilder.DropColumn(
                name: "IsOutOfCity",
                table: "Citizens");
        }
    }
}

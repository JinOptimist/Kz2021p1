using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication1.Migrations
{
    public partial class m2mPassengersFlights : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CitizenFlight");

            migrationBuilder.CreateTable(
                name: "Passenger",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CitizenId = table.Column<long>(type: "bigint", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Age = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Passenger", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Passenger_Citizens_CitizenId",
                        column: x => x.CitizenId,
                        principalTable: "Citizens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FlightPassenger",
                columns: table => new
                {
                    FlightsId = table.Column<long>(type: "bigint", nullable: false),
                    PassengersId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlightPassenger", x => new { x.FlightsId, x.PassengersId });
                    table.ForeignKey(
                        name: "FK_FlightPassenger_Flights_FlightsId",
                        column: x => x.FlightsId,
                        principalTable: "Flights",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FlightPassenger_Passenger_PassengersId",
                        column: x => x.PassengersId,
                        principalTable: "Passenger",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FlightPassenger_PassengersId",
                table: "FlightPassenger",
                column: "PassengersId");

            migrationBuilder.CreateIndex(
                name: "IX_Passenger_CitizenId",
                table: "Passenger",
                column: "CitizenId",
                unique: true,
                filter: "[CitizenId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FlightPassenger");

            migrationBuilder.DropTable(
                name: "Passenger");

            migrationBuilder.CreateTable(
                name: "CitizenFlight",
                columns: table => new
                {
                    CitizensId = table.Column<long>(type: "bigint", nullable: false),
                    FlightsId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CitizenFlight", x => new { x.CitizensId, x.FlightsId });
                    table.ForeignKey(
                        name: "FK_CitizenFlight_Citizens_CitizensId",
                        column: x => x.CitizensId,
                        principalTable: "Citizens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CitizenFlight_Flights_FlightsId",
                        column: x => x.FlightsId,
                        principalTable: "Flights",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CitizenFlight_FlightsId",
                table: "CitizenFlight",
                column: "FlightsId");
        }
    }
}

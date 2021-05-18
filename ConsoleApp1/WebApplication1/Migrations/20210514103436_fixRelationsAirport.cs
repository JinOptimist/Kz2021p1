using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication1.Migrations
{
    public partial class fixRelationsAirport : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DepartingFlightsInfo");

            migrationBuilder.DropTable(
                name: "IncomingFlightsInfo");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Passengers_Citizens_CitizenId",
            //    table: "Passengers",
            //    column: "CitizenId",
            //    principalTable: "Citizens",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Passengers_Flights_FlightId",
            //    table: "Passengers",
            //    column: "FlightId",
            //    principalTable: "Flights",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_Passengers_Citizens_CitizenId",
            //    table: "Passengers");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_Passengers_Flights_FlightId",
            //    table: "Passengers");

            migrationBuilder.CreateTable(
                name: "DepartingFlightsInfo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Airline = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DepartureTime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Destination = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FlightId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepartingFlightsInfo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IncomingFlightsInfo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Airline = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ETA = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FlightId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Origin = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IncomingFlightsInfo", x => x.Id);
                });
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication1.Migrations
{
    public partial class PoliceCall : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PoliceCallHistory",
                schema: "Police",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateCall = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CallPriority = table.Column<int>(type: "int", nullable: false),
                    CitizenId = table.Column<long>(type: "bigint", nullable: false),
                    PolicemanId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PoliceCallHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PoliceCallHistory_Citizens_CitizenId",
                        column: x => x.CitizenId,
                        principalTable: "Citizens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PoliceCallHistory_Policemen_PolicemanId",
                        column: x => x.PolicemanId,
                        principalSchema: "Police",
                        principalTable: "Policemen",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PoliceCallHistory_CitizenId",
                schema: "Police",
                table: "PoliceCallHistory",
                column: "CitizenId");

            migrationBuilder.CreateIndex(
                name: "IX_PoliceCallHistory_PolicemanId",
                schema: "Police",
                table: "PoliceCallHistory",
                column: "PolicemanId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PoliceCallHistory",
                schema: "Police");
        }
    }
}

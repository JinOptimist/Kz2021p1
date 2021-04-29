using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication1.Migrations
{
    public partial class AddFireIncident : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FireIncidents",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Injured = table.Column<int>(type: "int", nullable: false),
                    Dead = table.Column<int>(type: "int", nullable: false),
                    TeamId = table.Column<long>(type: "bigint", nullable: false),
                    FiremanId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FireIncidents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FireIncidents_FiremanTeams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "FiremanTeams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FireIncidents_Firemen_FiremanId",
                        column: x => x.FiremanId,
                        principalTable: "Firemen",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FireIncidents_FiremanId",
                table: "FireIncidents",
                column: "FiremanId");

            migrationBuilder.CreateIndex(
                name: "IX_FireIncidents_TeamId",
                table: "FireIncidents",
                column: "TeamId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FireIncidents");
        }
    }
}

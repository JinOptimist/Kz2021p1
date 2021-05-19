using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication1.Migrations
{
    public partial class ForeignKeyNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FireIncidents_FiremanTeams_TeamId",
                table: "FireIncidents");

            migrationBuilder.DropForeignKey(
                name: "FK_FiremanTeams_FireTrucks_TruckId",
                table: "FiremanTeams");

            migrationBuilder.DropIndex(
                name: "IX_FiremanTeams_TruckId",
                table: "FiremanTeams");

            migrationBuilder.AlterColumn<long>(
                name: "TruckId",
                table: "FiremanTeams",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "TeamId",
                table: "FireIncidents",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.CreateIndex(
                name: "IX_FiremanTeams_TruckId",
                table: "FiremanTeams",
                column: "TruckId",
                unique: true,
                filter: "[TruckId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_FireIncidents_FiremanTeams_TeamId",
                table: "FireIncidents",
                column: "TeamId",
                principalTable: "FiremanTeams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FiremanTeams_FireTrucks_TruckId",
                table: "FiremanTeams",
                column: "TruckId",
                principalTable: "FireTrucks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FireIncidents_FiremanTeams_TeamId",
                table: "FireIncidents");

            migrationBuilder.DropForeignKey(
                name: "FK_FiremanTeams_FireTrucks_TruckId",
                table: "FiremanTeams");

            migrationBuilder.DropIndex(
                name: "IX_FiremanTeams_TruckId",
                table: "FiremanTeams");

            migrationBuilder.AlterColumn<long>(
                name: "TruckId",
                table: "FiremanTeams",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "TeamId",
                table: "FireIncidents",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FiremanTeams_TruckId",
                table: "FiremanTeams",
                column: "TruckId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_FireIncidents_FiremanTeams_TeamId",
                table: "FireIncidents",
                column: "TeamId",
                principalTable: "FiremanTeams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FiremanTeams_FireTrucks_TruckId",
                table: "FiremanTeams",
                column: "TruckId",
                principalTable: "FireTrucks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

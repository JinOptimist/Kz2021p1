using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication1.Migrations
{
    public partial class AddElectionsUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CandidateElection");

            migrationBuilder.AddColumn<long>(
                name: "CandidateId",
                table: "Elections",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ElectionId",
                table: "Candidates",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Elections_CandidateId",
                table: "Elections",
                column: "CandidateId");

            migrationBuilder.CreateIndex(
                name: "IX_Candidates_ElectionId",
                table: "Candidates",
                column: "ElectionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Candidates_Elections_ElectionId",
                table: "Candidates",
                column: "ElectionId",
                principalTable: "Elections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Elections_Candidates_CandidateId",
                table: "Elections",
                column: "CandidateId",
                principalTable: "Candidates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Candidates_Elections_ElectionId",
                table: "Candidates");

            migrationBuilder.DropForeignKey(
                name: "FK_Elections_Candidates_CandidateId",
                table: "Elections");

            migrationBuilder.DropIndex(
                name: "IX_Elections_CandidateId",
                table: "Elections");

            migrationBuilder.DropIndex(
                name: "IX_Candidates_ElectionId",
                table: "Candidates");

            migrationBuilder.DropColumn(
                name: "CandidateId",
                table: "Elections");

            migrationBuilder.DropColumn(
                name: "ElectionId",
                table: "Candidates");

            migrationBuilder.CreateTable(
                name: "CandidateElection",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CandidateId = table.Column<long>(type: "bigint", nullable: false),
                    CandidateRegistrationTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    ElectionId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CandidateElection", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CandidateElection_Candidates_CandidateId",
                        column: x => x.CandidateId,
                        principalTable: "Candidates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CandidateElection_Elections_ElectionId",
                        column: x => x.ElectionId,
                        principalTable: "Elections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CandidateElection_CandidateId",
                table: "CandidateElection",
                column: "CandidateId");

            migrationBuilder.CreateIndex(
                name: "IX_CandidateElection_ElectionId",
                table: "CandidateElection",
                column: "ElectionId");
        }
    }
}

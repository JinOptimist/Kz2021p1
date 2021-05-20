using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication1.Migrations
{
    public partial class AddElections : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "BallotId",
                table: "Citizens",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CandidateId",
                table: "Citizens",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Candidates",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Age = table.Column<int>(type: "int", nullable: false),
                    AvatarUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Job = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Slogan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<int>(type: "int", nullable: false),
                    Idea = table.Column<int>(type: "int", nullable: false),
                    CitizenId = table.Column<long>(type: "bigint", nullable: true),
                    ElectionId = table.Column<long>(type: "bigint", nullable: true),
                    BallotId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Candidates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Candidates_Citizens_CitizenId",
                        column: x => x.CitizenId,
                        principalTable: "Citizens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Elections",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BallotId = table.Column<long>(type: "bigint", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Start = table.Column<DateTime>(type: "datetime2", nullable: false),
                    End = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CandidateId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Elections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Elections_Candidates_CandidateId",
                        column: x => x.CandidateId,
                        principalTable: "Candidates",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Ballots",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ElectionId = table.Column<long>(type: "bigint", nullable: true),
                    CitizenId = table.Column<long>(type: "bigint", nullable: true),
                    CandidateId = table.Column<long>(type: "bigint", nullable: true),
                    VoteTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ballots", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ballots_Candidates_CandidateId",
                        column: x => x.CandidateId,
                        principalTable: "Candidates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ballots_Citizens_CitizenId",
                        column: x => x.CitizenId,
                        principalTable: "Citizens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ballots_Elections_ElectionId",
                        column: x => x.ElectionId,
                        principalTable: "Elections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Citizens_BallotId",
                table: "Citizens",
                column: "BallotId");

            migrationBuilder.CreateIndex(
                name: "IX_Citizens_CandidateId",
                table: "Citizens",
                column: "CandidateId");

            migrationBuilder.CreateIndex(
                name: "IX_Ballots_CandidateId",
                table: "Ballots",
                column: "CandidateId");

            migrationBuilder.CreateIndex(
                name: "IX_Ballots_CitizenId",
                table: "Ballots",
                column: "CitizenId");

            migrationBuilder.CreateIndex(
                name: "IX_Ballots_ElectionId",
                table: "Ballots",
                column: "ElectionId");

            migrationBuilder.CreateIndex(
                name: "IX_Candidates_BallotId",
                table: "Candidates",
                column: "BallotId");

            migrationBuilder.CreateIndex(
                name: "IX_Candidates_CitizenId",
                table: "Candidates",
                column: "CitizenId");

            migrationBuilder.CreateIndex(
                name: "IX_Candidates_ElectionId",
                table: "Candidates",
                column: "ElectionId");

            migrationBuilder.CreateIndex(
                name: "IX_Elections_BallotId",
                table: "Elections",
                column: "BallotId");

            migrationBuilder.CreateIndex(
                name: "IX_Elections_CandidateId",
                table: "Elections",
                column: "CandidateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Citizens_Ballots_BallotId",
                table: "Citizens",
                column: "BallotId",
                principalTable: "Ballots",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Citizens_Candidates_CandidateId",
                table: "Citizens",
                column: "CandidateId",
                principalTable: "Candidates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Candidates_Ballots_BallotId",
                table: "Candidates",
                column: "BallotId",
                principalTable: "Ballots",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Candidates_Elections_ElectionId",
                table: "Candidates",
                column: "ElectionId",
                principalTable: "Elections",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Elections_Ballots_BallotId",
                table: "Elections",
                column: "BallotId",
                principalTable: "Ballots",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Citizens_Ballots_BallotId",
                table: "Citizens");

            migrationBuilder.DropForeignKey(
                name: "FK_Citizens_Candidates_CandidateId",
                table: "Citizens");

            migrationBuilder.DropForeignKey(
                name: "FK_Ballots_Candidates_CandidateId",
                table: "Ballots");

            migrationBuilder.DropForeignKey(
                name: "FK_Elections_Candidates_CandidateId",
                table: "Elections");

            migrationBuilder.DropForeignKey(
                name: "FK_Ballots_Elections_ElectionId",
                table: "Ballots");

            migrationBuilder.DropTable(
                name: "Candidates");

            migrationBuilder.DropTable(
                name: "Elections");

            migrationBuilder.DropTable(
                name: "Ballots");

            migrationBuilder.DropIndex(
                name: "IX_Citizens_BallotId",
                table: "Citizens");

            migrationBuilder.DropIndex(
                name: "IX_Citizens_CandidateId",
                table: "Citizens");

            migrationBuilder.DropColumn(
                name: "BallotId",
                table: "Citizens");

            migrationBuilder.DropColumn(
                name: "CandidateId",
                table: "Citizens");
        }
    }
}

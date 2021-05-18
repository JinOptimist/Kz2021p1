using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication1.Migrations
{
    public partial class AddTelevision : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TvCelebrities",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Occupation = table.Column<int>(type: "int", nullable: false),
                    CitizenId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TvCelebrities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TvCelebrities_Citizens_CitizenId",
                        column: x => x.CitizenId,
                        principalTable: "Citizens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TvChannels",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WorkingFrom = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TvChannels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TvProgrammes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContentRating = table.Column<int>(type: "int", nullable: false),
                    TypeOfProgramme = table.Column<int>(type: "int", nullable: false),
                    AvatarUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChannelId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TvProgrammes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TvProgrammes_TvChannels_ChannelId",
                        column: x => x.ChannelId,
                        principalTable: "TvChannels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TvStaff",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Occupation = table.Column<int>(type: "int", nullable: false),
                    CitizenId = table.Column<long>(type: "bigint", nullable: false),
                    ChannelId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TvStaff", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TvStaff_Citizens_CitizenId",
                        column: x => x.CitizenId,
                        principalTable: "Citizens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TvStaff_TvChannels_ChannelId",
                        column: x => x.ChannelId,
                        principalTable: "TvChannels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TvProgrammeCelebrities",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProgrammeId = table.Column<long>(type: "bigint", nullable: true),
                    CelebrityId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TvProgrammeCelebrities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TvProgrammeCelebrities_TvCelebrities_CelebrityId",
                        column: x => x.CelebrityId,
                        principalTable: "TvCelebrities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TvProgrammeCelebrities_TvProgrammes_ProgrammeId",
                        column: x => x.ProgrammeId,
                        principalTable: "TvProgrammes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TvSchedules",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AiringTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProgrammeId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TvSchedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TvSchedules_TvProgrammes_ProgrammeId",
                        column: x => x.ProgrammeId,
                        principalTable: "TvProgrammes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TvProgrammeStaff",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProgrammeId = table.Column<long>(type: "bigint", nullable: true),
                    StaffId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TvProgrammeStaff", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TvProgrammeStaff_TvProgrammes_ProgrammeId",
                        column: x => x.ProgrammeId,
                        principalTable: "TvProgrammes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TvProgrammeStaff_TvStaff_StaffId",
                        column: x => x.StaffId,
                        principalTable: "TvStaff",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TvCelebrities_CitizenId",
                table: "TvCelebrities",
                column: "CitizenId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TvProgrammeCelebrities_CelebrityId",
                table: "TvProgrammeCelebrities",
                column: "CelebrityId");

            migrationBuilder.CreateIndex(
                name: "IX_TvProgrammeCelebrities_ProgrammeId",
                table: "TvProgrammeCelebrities",
                column: "ProgrammeId");

            migrationBuilder.CreateIndex(
                name: "IX_TvProgrammes_ChannelId",
                table: "TvProgrammes",
                column: "ChannelId");

            migrationBuilder.CreateIndex(
                name: "IX_TvProgrammeStaff_ProgrammeId",
                table: "TvProgrammeStaff",
                column: "ProgrammeId");

            migrationBuilder.CreateIndex(
                name: "IX_TvProgrammeStaff_StaffId",
                table: "TvProgrammeStaff",
                column: "StaffId");

            migrationBuilder.CreateIndex(
                name: "IX_TvSchedules_ProgrammeId",
                table: "TvSchedules",
                column: "ProgrammeId");

            migrationBuilder.CreateIndex(
                name: "IX_TvStaff_ChannelId",
                table: "TvStaff",
                column: "ChannelId");

            migrationBuilder.CreateIndex(
                name: "IX_TvStaff_CitizenId",
                table: "TvStaff",
                column: "CitizenId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TvProgrammeCelebrities");

            migrationBuilder.DropTable(
                name: "TvProgrammeStaff");

            migrationBuilder.DropTable(
                name: "TvSchedules");

            migrationBuilder.DropTable(
                name: "TvCelebrities");

            migrationBuilder.DropTable(
                name: "TvStaff");

            migrationBuilder.DropTable(
                name: "TvProgrammes");

            migrationBuilder.DropTable(
                name: "TvChannels");
        }
    }
}

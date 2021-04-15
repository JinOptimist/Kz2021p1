using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication1.Migrations
{
    public partial class AddBronResto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           
            migrationBuilder.CreateTable(
                name: "BronResto",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumberOfTables = table.Column<int>(type: "int", nullable: false),
                    PhUserNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CountOfVisitors = table.Column<int>(type: "int", nullable: false),
                    DateOfVisitors = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StateReservation = table.Column<bool>(type: "bit", nullable: false),
                    ObjectRestoId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BronResto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BronResto_Restorans_ObjectRestoId",
                        column: x => x.ObjectRestoId,
                        principalTable: "Restorans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BronResto_ObjectRestoId",
                table: "BronResto",
                column: "ObjectRestoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BronResto");

            migrationBuilder.CreateTable(
                name: "RolesResto",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolesResto", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UsersResto",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RoleId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersResto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsersResto_RolesResto_RoleId",
                        column: x => x.RoleId,
                        principalTable: "RolesResto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "RolesResto",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1L, "admin" });

            migrationBuilder.InsertData(
                table: "RolesResto",
                columns: new[] { "Id", "Name" },
                values: new object[] { 2L, "user" });

            migrationBuilder.InsertData(
                table: "UsersResto",
                columns: new[] { "Id", "Email", "Password", "RoleId" },
                values: new object[] { 1L, "admin@mail.ru", "123456", 1L });

            migrationBuilder.CreateIndex(
                name: "IX_UsersResto_RoleId",
                table: "UsersResto",
                column: "RoleId");
        }
    }
}

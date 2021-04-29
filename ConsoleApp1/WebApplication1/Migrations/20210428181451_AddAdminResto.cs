using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication1.Migrations
{
    public partial class AddAdminResto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AdminResto",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LoginAdmin = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PasswordAdmin = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CitizenId = table.Column<long>(type: "bigint", nullable: false),
                    RestoranId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminResto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdminResto_Citizens_CitizenId",
                        column: x => x.CitizenId,
                        principalTable: "Citizens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AdminResto_Restorans_RestoranId",
                        column: x => x.RestoranId,
                        principalTable: "Restorans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdminResto_CitizenId",
                table: "AdminResto",
                column: "CitizenId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AdminResto_RestoranId",
                table: "AdminResto",
                column: "RestoranId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdminResto");
        }
    }
}

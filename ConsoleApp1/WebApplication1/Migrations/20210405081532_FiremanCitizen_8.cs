using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication1.Migrations
{
    public partial class FiremanCitizen_8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fireman_Citizens_Id",
                table: "Fireman");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "Fireman",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<long>(
                name: "CitizenId_",
                table: "Fireman",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Fireman_CitizenId_",
                table: "Fireman",
                column: "CitizenId_",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Fireman_Citizens_CitizenId_",
                table: "Fireman",
                column: "CitizenId_",
                principalTable: "Citizens",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fireman_Citizens_CitizenId_",
                table: "Fireman");

            migrationBuilder.DropIndex(
                name: "IX_Fireman_CitizenId_",
                table: "Fireman");

            migrationBuilder.DropColumn(
                name: "CitizenId_",
                table: "Fireman");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "Fireman",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddForeignKey(
                name: "FK_Fireman_Citizens_Id",
                table: "Fireman",
                column: "Id",
                principalTable: "Citizens",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

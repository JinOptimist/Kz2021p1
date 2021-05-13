using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication1.Migrations
{
    public partial class UpdateEducationTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CertificatePupil");

            migrationBuilder.DropTable(
                name: "DepartingFlightsInfo");

            migrationBuilder.DropTable(
                name: "IncomingFlightsInfo");

            migrationBuilder.RenameColumn(
                name: "IIN",
                table: "Students",
                newName: "Iin");

            migrationBuilder.RenameColumn(
                name: "OnGrant",
                table: "Students",
                newName: "IsGrant");

            migrationBuilder.RenameColumn(
                name: "Avatar",
                table: "Students",
                newName: "AvatarUrl");

            migrationBuilder.RenameColumn(
                name: "IIN",
                table: "Pupils",
                newName: "Iin");

            migrationBuilder.RenameColumn(
                name: "Avatar",
                table: "Pupils",
                newName: "AvatarUrl");

            migrationBuilder.AlterColumn<string>(
                name: "Surname",
                table: "Students",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30);

            migrationBuilder.AlterColumn<string>(
                name: "Surname",
                table: "Pupils",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30);

            migrationBuilder.AddColumn<long>(
                name: "CertificateId",
                table: "Pupils",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CertificateImgUrl",
                table: "Certificates",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true);               
           
            migrationBuilder.CreateIndex(
                name: "IX_Pupils_CertificateId",
                table: "Pupils",
                column: "CertificateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pupils_Certificates_CertificateId",
                table: "Pupils",
                column: "CertificateId",
                principalTable: "Certificates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pupils_Certificates_CertificateId",
                table: "Pupils");

            migrationBuilder.DropIndex(
                name: "IX_Pupils_CertificateId",
                table: "Pupils");

            migrationBuilder.DropColumn(
                name: "CertificateId",
                table: "Pupils");

            migrationBuilder.DropColumn(
                name: "CertificateImgUrl",
                table: "Certificates");

            migrationBuilder.RenameColumn(
                name: "Iin",
                table: "Students",
                newName: "IIN");

            migrationBuilder.RenameColumn(
                name: "IsGrant",
                table: "Students",
                newName: "OnGrant");

            migrationBuilder.RenameColumn(
                name: "AvatarUrl",
                table: "Students",
                newName: "Avatar");

            migrationBuilder.RenameColumn(
                name: "Iin",
                table: "Pupils",
                newName: "IIN");

            migrationBuilder.RenameColumn(
                name: "AvatarUrl",
                table: "Pupils",
                newName: "Avatar");

            migrationBuilder.AlterColumn<string>(
                name: "Surname",
                table: "Students",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Surname",
                table: "Pupils",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30,
                oldNullable: true);   

            migrationBuilder.CreateTable(
                name: "CertificatePupil",
                columns: table => new
                {
                    CertificatesId = table.Column<long>(type: "bigint", nullable: false),
                    PupilsId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CertificatePupil", x => new { x.CertificatesId, x.PupilsId });
                    table.ForeignKey(
                        name: "FK_CertificatePupil_Certificates_CertificatesId",
                        column: x => x.CertificatesId,
                        principalTable: "Certificates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CertificatePupil_Pupils_PupilsId",
                        column: x => x.PupilsId,
                        principalTable: "Pupils",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_CertificatePupil_PupilsId",
                table: "CertificatePupil",
                column: "PupilsId");
        }
    }
}

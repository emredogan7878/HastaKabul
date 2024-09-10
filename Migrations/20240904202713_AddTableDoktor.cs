using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HospitalApp.Migrations
{
    /// <inheritdoc />
    public partial class AddTableDoktor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DoktorId",
                table: "Bolumler",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Doktorlar",
                columns: table => new
                {
                    DoktorId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DoktorAd = table.Column<string>(type: "TEXT", nullable: true),
                    DoktorSoyad = table.Column<string>(type: "TEXT", nullable: true),
                    DoktorEposta = table.Column<string>(type: "TEXT", nullable: true),
                    DoktorTelefon = table.Column<string>(type: "TEXT", nullable: true),
                    BaslamaTarihi = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doktorlar", x => x.DoktorId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Randevular_BolumId",
                table: "Randevular",
                column: "BolumId");

            migrationBuilder.CreateIndex(
                name: "IX_Randevular_HastaId",
                table: "Randevular",
                column: "HastaId");

            migrationBuilder.CreateIndex(
                name: "IX_Bolumler_DoktorId",
                table: "Bolumler",
                column: "DoktorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bolumler_Doktorlar_DoktorId",
                table: "Bolumler",
                column: "DoktorId",
                principalTable: "Doktorlar",
                principalColumn: "DoktorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Randevular_Bolumler_BolumId",
                table: "Randevular",
                column: "BolumId",
                principalTable: "Bolumler",
                principalColumn: "BolumId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Randevular_Hastalar_HastaId",
                table: "Randevular",
                column: "HastaId",
                principalTable: "Hastalar",
                principalColumn: "HastaId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bolumler_Doktorlar_DoktorId",
                table: "Bolumler");

            migrationBuilder.DropForeignKey(
                name: "FK_Randevular_Bolumler_BolumId",
                table: "Randevular");

            migrationBuilder.DropForeignKey(
                name: "FK_Randevular_Hastalar_HastaId",
                table: "Randevular");

            migrationBuilder.DropTable(
                name: "Doktorlar");

            migrationBuilder.DropIndex(
                name: "IX_Randevular_BolumId",
                table: "Randevular");

            migrationBuilder.DropIndex(
                name: "IX_Randevular_HastaId",
                table: "Randevular");

            migrationBuilder.DropIndex(
                name: "IX_Bolumler_DoktorId",
                table: "Bolumler");

            migrationBuilder.DropColumn(
                name: "DoktorId",
                table: "Bolumler");
        }
    }
}

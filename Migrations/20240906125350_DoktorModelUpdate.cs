using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HospitalApp.Migrations
{
    /// <inheritdoc />
    public partial class DoktorModelUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bolumler_Doktorlar_DoktorId",
                table: "Bolumler");

            migrationBuilder.DropIndex(
                name: "IX_Bolumler_DoktorId",
                table: "Bolumler");

            migrationBuilder.DropColumn(
                name: "DoktorId",
                table: "Bolumler");

            migrationBuilder.AddColumn<int>(
                name: "BolumId",
                table: "Doktorlar",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Doktorlar_BolumId",
                table: "Doktorlar",
                column: "BolumId");

            migrationBuilder.AddForeignKey(
                name: "FK_Doktorlar_Bolumler_BolumId",
                table: "Doktorlar",
                column: "BolumId",
                principalTable: "Bolumler",
                principalColumn: "BolumId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Doktorlar_Bolumler_BolumId",
                table: "Doktorlar");

            migrationBuilder.DropIndex(
                name: "IX_Doktorlar_BolumId",
                table: "Doktorlar");

            migrationBuilder.DropColumn(
                name: "BolumId",
                table: "Doktorlar");

            migrationBuilder.AddColumn<int>(
                name: "DoktorId",
                table: "Bolumler",
                type: "INTEGER",
                nullable: true);

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
        }
    }
}

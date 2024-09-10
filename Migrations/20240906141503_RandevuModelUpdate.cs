using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HospitalApp.Migrations
{
    /// <inheritdoc />
    public partial class RandevuModelUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Randevular_Bolumler_BolumId",
                table: "Randevular");

            migrationBuilder.RenameColumn(
                name: "BolumId",
                table: "Randevular",
                newName: "DoktorId");

            migrationBuilder.RenameIndex(
                name: "IX_Randevular_BolumId",
                table: "Randevular",
                newName: "IX_Randevular_DoktorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Randevular_Doktorlar_DoktorId",
                table: "Randevular",
                column: "DoktorId",
                principalTable: "Doktorlar",
                principalColumn: "DoktorId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Randevular_Doktorlar_DoktorId",
                table: "Randevular");

            migrationBuilder.RenameColumn(
                name: "DoktorId",
                table: "Randevular",
                newName: "BolumId");

            migrationBuilder.RenameIndex(
                name: "IX_Randevular_DoktorId",
                table: "Randevular",
                newName: "IX_Randevular_BolumId");

            migrationBuilder.AddForeignKey(
                name: "FK_Randevular_Bolumler_BolumId",
                table: "Randevular",
                column: "BolumId",
                principalTable: "Bolumler",
                principalColumn: "BolumId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

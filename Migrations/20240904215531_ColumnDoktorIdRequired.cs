using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HospitalApp.Migrations
{
    /// <inheritdoc />
    public partial class ColumnDoktorIdRequired : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bolumler_Doktorlar_DoktorId",
                table: "Bolumler");

            migrationBuilder.AlterColumn<int>(
                name: "DoktorId",
                table: "Bolumler",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Bolumler_Doktorlar_DoktorId",
                table: "Bolumler",
                column: "DoktorId",
                principalTable: "Doktorlar",
                principalColumn: "DoktorId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bolumler_Doktorlar_DoktorId",
                table: "Bolumler");

            migrationBuilder.AlterColumn<int>(
                name: "DoktorId",
                table: "Bolumler",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_Bolumler_Doktorlar_DoktorId",
                table: "Bolumler",
                column: "DoktorId",
                principalTable: "Doktorlar",
                principalColumn: "DoktorId");
        }
    }
}

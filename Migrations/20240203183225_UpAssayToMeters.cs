using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tashtibaat.Migrations
{
    /// <inheritdoc />
    public partial class UpAssayToMeters : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssayToMeters_Assays_DesignsId",
                table: "AssayToMeters");

            migrationBuilder.RenameColumn(
                name: "DesignsId",
                table: "AssayToMeters",
                newName: "AssayId");

            migrationBuilder.RenameIndex(
                name: "IX_AssayToMeters_DesignsId",
                table: "AssayToMeters",
                newName: "IX_AssayToMeters_AssayId");

            migrationBuilder.AddForeignKey(
                name: "FK_AssayToMeters_Assays_AssayId",
                table: "AssayToMeters",
                column: "AssayId",
                principalTable: "Assays",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssayToMeters_Assays_AssayId",
                table: "AssayToMeters");

            migrationBuilder.RenameColumn(
                name: "AssayId",
                table: "AssayToMeters",
                newName: "DesignsId");

            migrationBuilder.RenameIndex(
                name: "IX_AssayToMeters_AssayId",
                table: "AssayToMeters",
                newName: "IX_AssayToMeters_DesignsId");

            migrationBuilder.AddForeignKey(
                name: "FK_AssayToMeters_Assays_DesignsId",
                table: "AssayToMeters",
                column: "DesignsId",
                principalTable: "Assays",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

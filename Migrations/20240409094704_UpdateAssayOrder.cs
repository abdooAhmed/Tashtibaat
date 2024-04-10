using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tashtibaat.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAssayOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Number",
                table: "AssayToMeters");

            migrationBuilder.AddColumn<float>(
                name: "Quantity",
                table: "AssayToMeters",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "AssayToMeters");

            migrationBuilder.AddColumn<int>(
                name: "Number",
                table: "AssayToMeters",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}

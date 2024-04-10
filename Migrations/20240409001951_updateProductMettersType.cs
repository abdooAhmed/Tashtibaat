using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tashtibaat.Migrations
{
    /// <inheritdoc />
    public partial class updateProductMettersType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Number",
                table: "ProductToMeters");

            migrationBuilder.AddColumn<float>(
                name: "Metters",
                table: "ProductToMeters",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Metters",
                table: "ProductToMeters");

            migrationBuilder.AddColumn<int>(
                name: "Number",
                table: "ProductToMeters",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}

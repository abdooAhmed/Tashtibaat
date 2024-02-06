using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tashtibaat.Migrations
{
    /// <inheritdoc />
    public partial class AssayUnit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Unit",
                table: "Assays",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Unit",
                table: "Assays");
        }
    }
}

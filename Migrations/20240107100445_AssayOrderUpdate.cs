using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tashtibaat.Migrations
{
    /// <inheritdoc />
    public partial class AssayOrderUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "AssayOrders",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AssayOrders_UserId",
                table: "AssayOrders",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AssayOrders_AspNetUsers_UserId",
                table: "AssayOrders",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssayOrders_AspNetUsers_UserId",
                table: "AssayOrders");

            migrationBuilder.DropIndex(
                name: "IX_AssayOrders_UserId",
                table: "AssayOrders");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "AssayOrders");
        }
    }
}

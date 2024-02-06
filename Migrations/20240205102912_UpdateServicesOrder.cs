using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tashtibaat.Migrations
{
    /// <inheritdoc />
    public partial class UpdateServicesOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "ServicesOrders",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ServicesOrders_UserId",
                table: "ServicesOrders",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ServicesOrders_AspNetUsers_UserId",
                table: "ServicesOrders",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServicesOrders_AspNetUsers_UserId",
                table: "ServicesOrders");

            migrationBuilder.DropIndex(
                name: "IX_ServicesOrders_UserId",
                table: "ServicesOrders");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ServicesOrders");
        }
    }
}

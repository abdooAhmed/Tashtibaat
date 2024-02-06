using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tashtibaat.Migrations
{
    /// <inheritdoc />
    public partial class upToMeters : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DesignOrders_DesignOrders_DesignOrderId",
                table: "DesignOrders");

            migrationBuilder.DropIndex(
                name: "IX_DesignOrders_DesignOrderId",
                table: "DesignOrders");

            migrationBuilder.DropColumn(
                name: "DesignOrderId",
                table: "DesignOrders");

            migrationBuilder.AddColumn<int>(
                name: "DesignOrderId",
                table: "DesignToMeters",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DesignToMeters_DesignOrderId",
                table: "DesignToMeters",
                column: "DesignOrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_DesignToMeters_DesignOrders_DesignOrderId",
                table: "DesignToMeters",
                column: "DesignOrderId",
                principalTable: "DesignOrders",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DesignToMeters_DesignOrders_DesignOrderId",
                table: "DesignToMeters");

            migrationBuilder.DropIndex(
                name: "IX_DesignToMeters_DesignOrderId",
                table: "DesignToMeters");

            migrationBuilder.DropColumn(
                name: "DesignOrderId",
                table: "DesignToMeters");

            migrationBuilder.AddColumn<int>(
                name: "DesignOrderId",
                table: "DesignOrders",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DesignOrders_DesignOrderId",
                table: "DesignOrders",
                column: "DesignOrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_DesignOrders_DesignOrders_DesignOrderId",
                table: "DesignOrders",
                column: "DesignOrderId",
                principalTable: "DesignOrders",
                principalColumn: "Id");
        }
    }
}

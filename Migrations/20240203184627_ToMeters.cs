using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tashtibaat.Migrations
{
    /// <inheritdoc />
    public partial class ToMeters : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Designs_DesignOrders_DesignOrderId",
                table: "Designs");

            migrationBuilder.DropTable(
                name: "OrderProducts");

            migrationBuilder.DropIndex(
                name: "IX_Designs_DesignOrderId",
                table: "Designs");

            migrationBuilder.DropColumn(
                name: "DesignOrderId",
                table: "Designs");

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "ProductToMeters",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductsId",
                table: "Orders",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DesignOrderId",
                table: "DesignOrders",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductToMeters_OrderId",
                table: "ProductToMeters",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ProductsId",
                table: "Orders",
                column: "ProductsId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Products_ProductsId",
                table: "Orders",
                column: "ProductsId",
                principalTable: "Products",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductToMeters_Orders_OrderId",
                table: "ProductToMeters",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DesignOrders_DesignOrders_DesignOrderId",
                table: "DesignOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Products_ProductsId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductToMeters_Orders_OrderId",
                table: "ProductToMeters");

            migrationBuilder.DropIndex(
                name: "IX_ProductToMeters_OrderId",
                table: "ProductToMeters");

            migrationBuilder.DropIndex(
                name: "IX_Orders_ProductsId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_DesignOrders_DesignOrderId",
                table: "DesignOrders");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "ProductToMeters");

            migrationBuilder.DropColumn(
                name: "ProductsId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "DesignOrderId",
                table: "DesignOrders");

            migrationBuilder.AddColumn<int>(
                name: "DesignOrderId",
                table: "Designs",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "OrderProducts",
                columns: table => new
                {
                    OrdersId = table.Column<int>(type: "int", nullable: false),
                    ProductsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderProducts", x => new { x.OrdersId, x.ProductsId });
                    table.ForeignKey(
                        name: "FK_OrderProducts_Orders_OrdersId",
                        column: x => x.OrdersId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderProducts_Products_ProductsId",
                        column: x => x.ProductsId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Designs_DesignOrderId",
                table: "Designs",
                column: "DesignOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderProducts_ProductsId",
                table: "OrderProducts",
                column: "ProductsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Designs_DesignOrders_DesignOrderId",
                table: "Designs",
                column: "DesignOrderId",
                principalTable: "DesignOrders",
                principalColumn: "Id");
        }
    }
}

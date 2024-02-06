using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tashtibaat.Migrations
{
    /// <inheritdoc />
    public partial class AssayToMeters : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assays_AssayOrders_AssayOrderId",
                table: "Assays");

            migrationBuilder.DropIndex(
                name: "IX_Assays_AssayOrderId",
                table: "Assays");

            migrationBuilder.DropColumn(
                name: "AssayOrderId",
                table: "Assays");

            migrationBuilder.CreateTable(
                name: "AssayToMeters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<int>(type: "int", nullable: false),
                    DesignsId = table.Column<int>(type: "int", nullable: false),
                    AssayOrderId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssayToMeters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssayToMeters_AssayOrders_AssayOrderId",
                        column: x => x.AssayOrderId,
                        principalTable: "AssayOrders",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AssayToMeters_Assays_DesignsId",
                        column: x => x.DesignsId,
                        principalTable: "Assays",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DesignToMeters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<int>(type: "int", nullable: false),
                    DesignsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DesignToMeters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DesignToMeters_Designs_DesignsId",
                        column: x => x.DesignsId,
                        principalTable: "Designs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductToMeters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<int>(type: "int", nullable: false),
                    ProductsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductToMeters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductToMeters_Products_ProductsId",
                        column: x => x.ProductsId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AssayToMeters_AssayOrderId",
                table: "AssayToMeters",
                column: "AssayOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_AssayToMeters_DesignsId",
                table: "AssayToMeters",
                column: "DesignsId");

            migrationBuilder.CreateIndex(
                name: "IX_DesignToMeters_DesignsId",
                table: "DesignToMeters",
                column: "DesignsId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductToMeters_ProductsId",
                table: "ProductToMeters",
                column: "ProductsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssayToMeters");

            migrationBuilder.DropTable(
                name: "DesignToMeters");

            migrationBuilder.DropTable(
                name: "ProductToMeters");

            migrationBuilder.AddColumn<int>(
                name: "AssayOrderId",
                table: "Assays",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Assays_AssayOrderId",
                table: "Assays",
                column: "AssayOrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Assays_AssayOrders_AssayOrderId",
                table: "Assays",
                column: "AssayOrderId",
                principalTable: "AssayOrders",
                principalColumn: "Id");
        }
    }
}

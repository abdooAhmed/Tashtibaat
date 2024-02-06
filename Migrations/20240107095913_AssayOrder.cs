using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tashtibaat.Migrations
{
    /// <inheritdoc />
    public partial class AssayOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AssayOrderId",
                table: "Assays",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AssayOrders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Total = table.Column<int>(type: "int", nullable: false),
                    Cash = table.Column<bool>(type: "bit", nullable: false),
                    Bank = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssayOrders", x => x.Id);
                });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assays_AssayOrders_AssayOrderId",
                table: "Assays");

            migrationBuilder.DropTable(
                name: "AssayOrders");

            migrationBuilder.DropIndex(
                name: "IX_Assays_AssayOrderId",
                table: "Assays");

            migrationBuilder.DropColumn(
                name: "AssayOrderId",
                table: "Assays");
        }
    }
}

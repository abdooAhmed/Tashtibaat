using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tashtibaat.Migrations
{
    /// <inheritdoc />
    public partial class DesignOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DesignOrderId",
                table: "Designs",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DesignOrders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Total = table.Column<int>(type: "int", nullable: false),
                    Cash = table.Column<bool>(type: "bit", nullable: false),
                    Bank = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DesignOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DesignOrders_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Designs_DesignOrderId",
                table: "Designs",
                column: "DesignOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_DesignOrders_UserId",
                table: "DesignOrders",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Designs_DesignOrders_DesignOrderId",
                table: "Designs",
                column: "DesignOrderId",
                principalTable: "DesignOrders",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Designs_DesignOrders_DesignOrderId",
                table: "Designs");

            migrationBuilder.DropTable(
                name: "DesignOrders");

            migrationBuilder.DropIndex(
                name: "IX_Designs_DesignOrderId",
                table: "Designs");

            migrationBuilder.DropColumn(
                name: "DesignOrderId",
                table: "Designs");
        }
    }
}

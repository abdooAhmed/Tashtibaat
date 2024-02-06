using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tashtibaat.Migrations
{
    /// <inheritdoc />
    public partial class ServicesOrders : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ServicesOrders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PhoneNubmer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServicesOrders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ArmoredDoorTOOrders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArmoredDoorId = table.Column<int>(type: "int", nullable: false),
                    ServicesOrderId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArmoredDoorTOOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ArmoredDoorTOOrders_ArmoredDoors_ArmoredDoorId",
                        column: x => x.ArmoredDoorId,
                        principalTable: "ArmoredDoors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArmoredDoorTOOrders_ServicesOrders_ServicesOrderId",
                        column: x => x.ServicesOrderId,
                        principalTable: "ServicesOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DressingRoomToOrders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DressingRoomId = table.Column<int>(type: "int", nullable: false),
                    Space = table.Column<float>(type: "real", nullable: false),
                    ServicesOrderId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DressingRoomToOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DressingRoomToOrders_DressingRooms_DressingRoomId",
                        column: x => x.DressingRoomId,
                        principalTable: "DressingRooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DressingRoomToOrders_ServicesOrders_ServicesOrderId",
                        column: x => x.ServicesOrderId,
                        principalTable: "ServicesOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KitchenToOrders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Space = table.Column<float>(type: "real", nullable: false),
                    KitchenId = table.Column<int>(type: "int", nullable: false),
                    ServicesOrderId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KitchenToOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KitchenToOrders_Kitchens_KitchenId",
                        column: x => x.KitchenId,
                        principalTable: "Kitchens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KitchenToOrders_ServicesOrders_ServicesOrderId",
                        column: x => x.ServicesOrderId,
                        principalTable: "ServicesOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SecuritySystemToOrders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SecuritySurveillanceId = table.Column<int>(type: "int", nullable: false),
                    ServicesOrderId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SecuritySystemToOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SecuritySystemToOrders_SecuritySurveillances_SecuritySurveillanceId",
                        column: x => x.SecuritySurveillanceId,
                        principalTable: "SecuritySurveillances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SecuritySystemToOrders_ServicesOrders_ServicesOrderId",
                        column: x => x.ServicesOrderId,
                        principalTable: "ServicesOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArmoredDoorTOOrders_ArmoredDoorId",
                table: "ArmoredDoorTOOrders",
                column: "ArmoredDoorId");

            migrationBuilder.CreateIndex(
                name: "IX_ArmoredDoorTOOrders_ServicesOrderId",
                table: "ArmoredDoorTOOrders",
                column: "ServicesOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_DressingRoomToOrders_DressingRoomId",
                table: "DressingRoomToOrders",
                column: "DressingRoomId");

            migrationBuilder.CreateIndex(
                name: "IX_DressingRoomToOrders_ServicesOrderId",
                table: "DressingRoomToOrders",
                column: "ServicesOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_KitchenToOrders_KitchenId",
                table: "KitchenToOrders",
                column: "KitchenId");

            migrationBuilder.CreateIndex(
                name: "IX_KitchenToOrders_ServicesOrderId",
                table: "KitchenToOrders",
                column: "ServicesOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_SecuritySystemToOrders_SecuritySurveillanceId",
                table: "SecuritySystemToOrders",
                column: "SecuritySurveillanceId");

            migrationBuilder.CreateIndex(
                name: "IX_SecuritySystemToOrders_ServicesOrderId",
                table: "SecuritySystemToOrders",
                column: "ServicesOrderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArmoredDoorTOOrders");

            migrationBuilder.DropTable(
                name: "DressingRoomToOrders");

            migrationBuilder.DropTable(
                name: "KitchenToOrders");

            migrationBuilder.DropTable(
                name: "SecuritySystemToOrders");

            migrationBuilder.DropTable(
                name: "ServicesOrders");
        }
    }
}

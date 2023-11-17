using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StockControl.Server.Migrations
{
    /// <inheritdoc />
    public partial class RestOfDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EquiposRepuestos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SparePartsId_Code = table.Column<int>(type: "int", nullable: false),
                    EquipoId_Equip = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquiposRepuestos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EquiposRepuestos_Equipos_EquipoId_Equip",
                        column: x => x.EquipoId_Equip,
                        principalTable: "Equipos",
                        principalColumn: "Id_Equip",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EquiposRepuestos_SpareParts_SparePartsId_Code",
                        column: x => x.SparePartsId_Code,
                        principalTable: "SpareParts",
                        principalColumn: "Id_Code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrdenesTotales",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    nOrden = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Escuela = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Hour = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    isFlash = table.Column<bool>(type: "bit", nullable: false),
                    isFlashCap = table.Column<bool>(type: "bit", nullable: false),
                    isFinished = table.Column<int>(type: "int", nullable: false),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserNameId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    EquipoId_Equip = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdenesTotales", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrdenesTotales_ApplicationUser_UserNameId",
                        column: x => x.UserNameId,
                        principalTable: "ApplicationUser",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OrdenesTotales_Equipos_EquipoId_Equip",
                        column: x => x.EquipoId_Equip,
                        principalTable: "Equipos",
                        principalColumn: "Id_Equip",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderSpareParts",
                columns: table => new
                {
                    SparePartId = table.Column<int>(type: "int", nullable: false),
                    OrderTotalId = table.Column<int>(type: "int", nullable: false),
                    SparePartsId_Code = table.Column<int>(type: "int", nullable: false),
                    OrdersTotalsId = table.Column<int>(type: "int", nullable: false),
                    TipoStockId_Stock = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderSpareParts", x => new { x.SparePartId, x.OrderTotalId });
                    table.ForeignKey(
                        name: "FK_OrderSpareParts_OrdenesTotales_OrdersTotalsId",
                        column: x => x.OrdersTotalsId,
                        principalTable: "OrdenesTotales",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderSpareParts_SpareParts_SparePartsId_Code",
                        column: x => x.SparePartsId_Code,
                        principalTable: "SpareParts",
                        principalColumn: "Id_Code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderSpareParts_TipoStock_TipoStockId_Stock",
                        column: x => x.TipoStockId_Stock,
                        principalTable: "TipoStock",
                        principalColumn: "Id_Stock",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EquiposRepuestos_EquipoId_Equip",
                table: "EquiposRepuestos",
                column: "EquipoId_Equip");

            migrationBuilder.CreateIndex(
                name: "IX_EquiposRepuestos_SparePartsId_Code",
                table: "EquiposRepuestos",
                column: "SparePartsId_Code");

            migrationBuilder.CreateIndex(
                name: "IX_OrdenesTotales_EquipoId_Equip",
                table: "OrdenesTotales",
                column: "EquipoId_Equip");

            migrationBuilder.CreateIndex(
                name: "IX_OrdenesTotales_UserNameId",
                table: "OrdenesTotales",
                column: "UserNameId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderSpareParts_OrdersTotalsId",
                table: "OrderSpareParts",
                column: "OrdersTotalsId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderSpareParts_SparePartsId_Code",
                table: "OrderSpareParts",
                column: "SparePartsId_Code");

            migrationBuilder.CreateIndex(
                name: "IX_OrderSpareParts_TipoStockId_Stock",
                table: "OrderSpareParts",
                column: "TipoStockId_Stock");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EquiposRepuestos");

            migrationBuilder.DropTable(
                name: "OrderSpareParts");

            migrationBuilder.DropTable(
                name: "OrdenesTotales");
        }
    }
}

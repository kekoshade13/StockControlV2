using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StockControl.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddRepuestosEstados : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Repuestos");

            migrationBuilder.CreateTable(
                name: "RepuestosEstados",
                columns: table => new
                {
                    SparePartId = table.Column<int>(type: "int", nullable: false),
                    StockTypeId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RepuestosEstados", x => new { x.SparePartId, x.StockTypeId });
                    table.ForeignKey(
                        name: "FK_RepuestosEstados_SpareParts_SparePartId",
                        column: x => x.SparePartId,
                        principalTable: "SpareParts",
                        principalColumn: "Id_Code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RepuestosEstados_TipoStock_StockTypeId",
                        column: x => x.StockTypeId,
                        principalTable: "TipoStock",
                        principalColumn: "Id_Stock",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RepuestosEstados_StockTypeId",
                table: "RepuestosEstados",
                column: "StockTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RepuestosEstados");

            migrationBuilder.CreateTable(
                name: "Repuestos",
                columns: table => new
                {
                    Id_User = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Apellido = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CI = table.Column<int>(type: "int", nullable: false),
                    Class = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Genero = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nombre_u = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TargetProd = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Repuestos", x => x.Id_User);
                });
        }
    }
}

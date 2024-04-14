using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StockControl.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddActiveInEquipos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "Equipos",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Active",
                table: "Equipos");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StockControl.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddRoleInUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RoleId",
                table: "ApplicationUser",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "IdentityRole",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityRole", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUser_RoleId",
                table: "ApplicationUser",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUser_IdentityRole_RoleId",
                table: "ApplicationUser",
                column: "RoleId",
                principalTable: "IdentityRole",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUser_IdentityRole_RoleId",
                table: "ApplicationUser");

            migrationBuilder.DropTable(
                name: "IdentityRole");

            migrationBuilder.DropIndex(
                name: "IX_ApplicationUser_RoleId",
                table: "ApplicationUser");

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "ApplicationUser");
        }
    }
}

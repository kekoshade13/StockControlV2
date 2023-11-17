using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StockControl.Server.Migrations
{
    /// <inheritdoc />
    public partial class addedUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUser_IdentityRole_RoleId",
                table: "ApplicationUser");

            migrationBuilder.DropForeignKey(
                name: "FK_Movements_ApplicationUser_UserNameId",
                table: "Movements");

            migrationBuilder.DropForeignKey(
                name: "FK_OrdenesTotales_ApplicationUser_UserNameId",
                table: "OrdenesTotales");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ApplicationUser",
                table: "ApplicationUser");

            migrationBuilder.RenameTable(
                name: "ApplicationUser",
                newName: "Users");

            migrationBuilder.RenameIndex(
                name: "IX_ApplicationUser_RoleId",
                table: "Users",
                newName: "IX_Users_RoleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Movements_Users_UserNameId",
                table: "Movements",
                column: "UserNameId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrdenesTotales_Users_UserNameId",
                table: "OrdenesTotales",
                column: "UserNameId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_IdentityRole_RoleId",
                table: "Users",
                column: "RoleId",
                principalTable: "IdentityRole",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movements_Users_UserNameId",
                table: "Movements");

            migrationBuilder.DropForeignKey(
                name: "FK_OrdenesTotales_Users_UserNameId",
                table: "OrdenesTotales");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_IdentityRole_RoleId",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "ApplicationUser");

            migrationBuilder.RenameIndex(
                name: "IX_Users_RoleId",
                table: "ApplicationUser",
                newName: "IX_ApplicationUser_RoleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ApplicationUser",
                table: "ApplicationUser",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUser_IdentityRole_RoleId",
                table: "ApplicationUser",
                column: "RoleId",
                principalTable: "IdentityRole",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Movements_ApplicationUser_UserNameId",
                table: "Movements",
                column: "UserNameId",
                principalTable: "ApplicationUser",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrdenesTotales_ApplicationUser_UserNameId",
                table: "OrdenesTotales",
                column: "UserNameId",
                principalTable: "ApplicationUser",
                principalColumn: "Id");
        }
    }
}

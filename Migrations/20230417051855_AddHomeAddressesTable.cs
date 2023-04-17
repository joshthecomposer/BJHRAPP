using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BJHRApp.Migrations
{
    public partial class AddHomeAddressesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HomeAddress_Users_UserId",
                table: "HomeAddress");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HomeAddress",
                table: "HomeAddress");

            migrationBuilder.RenameTable(
                name: "HomeAddress",
                newName: "HomeAddresses");

            migrationBuilder.RenameIndex(
                name: "IX_HomeAddress_UserId",
                table: "HomeAddresses",
                newName: "IX_HomeAddresses_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HomeAddresses",
                table: "HomeAddresses",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_HomeAddresses_Users_UserId",
                table: "HomeAddresses",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HomeAddresses_Users_UserId",
                table: "HomeAddresses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HomeAddresses",
                table: "HomeAddresses");

            migrationBuilder.RenameTable(
                name: "HomeAddresses",
                newName: "HomeAddress");

            migrationBuilder.RenameIndex(
                name: "IX_HomeAddresses_UserId",
                table: "HomeAddress",
                newName: "IX_HomeAddress_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HomeAddress",
                table: "HomeAddress",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_HomeAddress_Users_UserId",
                table: "HomeAddress",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

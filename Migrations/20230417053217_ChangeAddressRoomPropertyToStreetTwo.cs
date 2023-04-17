using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BJHRApp.Migrations
{
    public partial class ChangeAddressRoomPropertyToStreetTwo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Room",
                table: "Locations",
                newName: "StreetTwo");

            migrationBuilder.RenameColumn(
                name: "Room",
                table: "HomeAddresses",
                newName: "StreetTwo");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StreetTwo",
                table: "Locations",
                newName: "Room");

            migrationBuilder.RenameColumn(
                name: "StreetTwo",
                table: "HomeAddresses",
                newName: "Room");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BJHRApp.Migrations
{
    public partial class ShiftRefactorMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "In",
                table: "Shifts");

            migrationBuilder.DropColumn(
                name: "Out",
                table: "Shifts");

            migrationBuilder.AddColumn<string>(
                name: "Block",
                table: "Shifts",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Date",
                table: "Shifts",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Block",
                table: "Shifts");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "Shifts");

            migrationBuilder.AddColumn<DateTime>(
                name: "In",
                table: "Shifts",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Out",
                table: "Shifts",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}

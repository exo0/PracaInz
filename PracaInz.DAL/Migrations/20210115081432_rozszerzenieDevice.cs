using Microsoft.EntityFrameworkCore.Migrations;

namespace PracaInz.DAL.Migrations
{
    public partial class rozszerzenieDevice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DeviceName",
                table: "Devices",
                newName: "SerialNumber");

            migrationBuilder.AddColumn<string>(
                name: "Manufacturer",
                table: "Devices",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Model",
                table: "Devices",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Manufacturer",
                table: "Devices");

            migrationBuilder.DropColumn(
                name: "Model",
                table: "Devices");

            migrationBuilder.RenameColumn(
                name: "SerialNumber",
                table: "Devices",
                newName: "DeviceName");
        }
    }
}

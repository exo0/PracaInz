using Microsoft.EntityFrameworkCore.Migrations;

namespace PracaInz.DAL.Migrations
{
    public partial class DodaniePolaisAliveNetworkDevice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isAlive",
                table: "Device",
                type: "bit",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isAlive",
                table: "Device");
        }
    }
}

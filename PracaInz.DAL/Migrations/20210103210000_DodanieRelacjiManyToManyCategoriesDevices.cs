using Microsoft.EntityFrameworkCore.Migrations;

namespace PracaInz.DAL.Migrations
{
    public partial class DodanieRelacjiManyToManyCategoriesDevices : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Devices_Categories_CategoryDeviceId",
                table: "Devices");

            migrationBuilder.DropIndex(
                name: "IX_Devices_CategoryDeviceId",
                table: "Devices");

            migrationBuilder.DropColumn(
                name: "CategoryDeviceId",
                table: "Devices");

            migrationBuilder.DropColumn(
                name: "ConfirmPassword",
                table: "AspNetUsers");

            migrationBuilder.CreateTable(
                name: "CategoryDevice",
                columns: table => new
                {
                    CategoriesId = table.Column<int>(type: "int", nullable: false),
                    DevicesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryDevice", x => new { x.CategoriesId, x.DevicesId });
                    table.ForeignKey(
                        name: "FK_CategoryDevice_Categories_CategoriesId",
                        column: x => x.CategoriesId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryDevice_Devices_DevicesId",
                        column: x => x.DevicesId,
                        principalTable: "Devices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategoryDevice_DevicesId",
                table: "CategoryDevice",
                column: "DevicesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryDevice");

            migrationBuilder.AddColumn<int>(
                name: "CategoryDeviceId",
                table: "Devices",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ConfirmPassword",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Devices_CategoryDeviceId",
                table: "Devices",
                column: "CategoryDeviceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Devices_Categories_CategoryDeviceId",
                table: "Devices",
                column: "CategoryDeviceId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

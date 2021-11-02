using Microsoft.EntityFrameworkCore.Migrations;

namespace TelecomProject.Data.Migrations
{
    public partial class Migration6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PersonDevice_PhoneNumber",
                table: "PersonDevice");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "PersonDevice");

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Devices",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Devices_PhoneNumber",
                table: "Devices",
                column: "PhoneNumber",
                unique: true,
                filter: "[PhoneNumber] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Devices_PhoneNumber",
                table: "Devices");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Devices");

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "PersonDevice",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PersonDevice_PhoneNumber",
                table: "PersonDevice",
                column: "PhoneNumber",
                unique: true,
                filter: "[PhoneNumber] IS NOT NULL");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace TelecomProject.Data.Migrations
{
    public partial class Migration4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountPlans_Accounts_AccountsAccountId1",
                table: "AccountPlans");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AccountPlans",
                table: "AccountPlans");

            migrationBuilder.RenameColumn(
                name: "AccountsAccountId1",
                table: "AccountPlans",
                newName: "AccountNumber");

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "logins",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "Devices",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeviceName",
                table: "Devices",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "features",
                table: "Devices",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_AccountPlans",
                table: "AccountPlans",
                columns: new[] { "AccountsAccountId", "PlanId" });

            migrationBuilder.CreateIndex(
                name: "IX_logins_Username",
                table: "logins",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Devices_PhoneNumber",
                table: "Devices",
                column: "PhoneNumber",
                unique: true,
                filter: "[PhoneNumber] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountPlans_Accounts_AccountsAccountId",
                table: "AccountPlans",
                column: "AccountsAccountId",
                principalTable: "Accounts",
                principalColumn: "AccountId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountPlans_Accounts_AccountsAccountId",
                table: "AccountPlans");

            migrationBuilder.DropIndex(
                name: "IX_logins_Username",
                table: "logins");

            migrationBuilder.DropIndex(
                name: "IX_Devices_PhoneNumber",
                table: "Devices");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AccountPlans",
                table: "AccountPlans");

            migrationBuilder.DropColumn(
                name: "DeviceName",
                table: "Devices");

            migrationBuilder.DropColumn(
                name: "features",
                table: "Devices");

            migrationBuilder.RenameColumn(
                name: "AccountNumber",
                table: "AccountPlans",
                newName: "AccountsAccountId1");

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "logins",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "Devices",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_AccountPlans",
                table: "AccountPlans",
                columns: new[] { "AccountsAccountId1", "PlanId" });

            migrationBuilder.AddForeignKey(
                name: "FK_AccountPlans_Accounts_AccountsAccountId1",
                table: "AccountPlans",
                column: "AccountsAccountId1",
                principalTable: "Accounts",
                principalColumn: "AccountId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

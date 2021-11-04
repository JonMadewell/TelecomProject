using Microsoft.EntityFrameworkCore.Migrations;

namespace TelecomProject.Data.Migrations
{
    public partial class Migration3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountPlans_Accounts_AccountsAccountId",
                table: "AccountPlans");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AccountPlans",
                table: "AccountPlans");

            migrationBuilder.RenameColumn(
                name: "AccountNumber",
                table: "AccountPlans",
                newName: "AccountsAccountId1");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddPrimaryKey(
                name: "PK_AccountPlans",
                table: "AccountPlans",
                columns: new[] { "AccountsAccountId", "PlanId" });

            migrationBuilder.AddForeignKey(
                name: "FK_AccountPlans_Accounts_AccountsAccountId",
                table: "AccountPlans",
                column: "AccountsAccountId",
                principalTable: "Accounts",
                principalColumn: "AccountId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

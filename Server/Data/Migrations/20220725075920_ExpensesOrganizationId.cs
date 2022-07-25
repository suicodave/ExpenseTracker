using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Data.Migrations
{
    public partial class ExpensesOrganizationId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrganizationId",
                table: "Expenses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_OrganizationId",
                table: "Expenses",
                column: "OrganizationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_Organizations_OrganizationId",
                table: "Expenses",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_Organizations_OrganizationId",
                table: "Expenses");

            migrationBuilder.DropIndex(
                name: "IX_Expenses_OrganizationId",
                table: "Expenses");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "Expenses");
        }
    }
}

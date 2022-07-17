using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Data.Migrations
{
    public partial class AccountTypeCompositeUnique : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AccountTypes_Name",
                table: "AccountTypes");

            migrationBuilder.CreateIndex(
                name: "IX_AccountTypes_Name_OrganizationId",
                table: "AccountTypes",
                columns: new[] { "Name", "OrganizationId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AccountTypes_Name_OrganizationId",
                table: "AccountTypes");

            migrationBuilder.CreateIndex(
                name: "IX_AccountTypes_Name",
                table: "AccountTypes",
                column: "Name",
                unique: true);
        }
    }
}

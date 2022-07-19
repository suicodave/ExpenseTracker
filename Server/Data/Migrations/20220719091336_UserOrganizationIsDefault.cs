using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Data.Migrations
{
    public partial class UserOrganizationIsDefault : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDefault",
                table: "Organizations");

            migrationBuilder.AddColumn<bool>(
                name: "IsDefault",
                table: "UserOrganizations",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDefault",
                table: "UserOrganizations");

            migrationBuilder.AddColumn<bool>(
                name: "IsDefault",
                table: "Organizations",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}

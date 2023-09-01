using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentGymKOI.Data.Migrations
{
    public partial class UserMembershipUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "UserMembership");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "UserMembership",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}

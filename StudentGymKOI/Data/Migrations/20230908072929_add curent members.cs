using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentGymKOI.Data.Migrations
{
    public partial class addcurentmembers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CurrentMembers",
                table: "GymClass",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentMembers",
                table: "GymClass");
        }
    }
}

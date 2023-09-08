using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentGymKOI.Data.Migrations
{
    public partial class UserGymClasstable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserGymClass",
                columns: table => new
                {
                    UserGymClassID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GymClassID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserGymClass", x => x.UserGymClassID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserGymClass");
        }
    }
}

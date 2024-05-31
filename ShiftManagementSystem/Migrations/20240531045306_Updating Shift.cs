using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShiftManagementSystem.Migrations
{
    public partial class UpdatingShift : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Shifts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Shifts");
        }
    }
}

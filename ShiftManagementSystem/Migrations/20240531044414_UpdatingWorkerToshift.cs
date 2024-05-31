using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShiftManagementSystem.Migrations
{
    public partial class UpdatingWorkerToshift : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_WorkerToShifts_ShiftId",
                table: "WorkerToShifts",
                column: "ShiftId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkerToShifts_WorkerId",
                table: "WorkerToShifts",
                column: "WorkerId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkerToShifts_Shifts_ShiftId",
                table: "WorkerToShifts",
                column: "ShiftId",
                principalTable: "Shifts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkerToShifts_Workers_WorkerId",
                table: "WorkerToShifts",
                column: "WorkerId",
                principalTable: "Workers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkerToShifts_Shifts_ShiftId",
                table: "WorkerToShifts");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkerToShifts_Workers_WorkerId",
                table: "WorkerToShifts");

            migrationBuilder.DropIndex(
                name: "IX_WorkerToShifts_ShiftId",
                table: "WorkerToShifts");

            migrationBuilder.DropIndex(
                name: "IX_WorkerToShifts_WorkerId",
                table: "WorkerToShifts");
        }
    }
}

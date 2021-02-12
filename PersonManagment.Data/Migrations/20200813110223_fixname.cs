using Microsoft.EntityFrameworkCore.Migrations;

namespace PersonManagment.Data.Migrations
{
    public partial class fixname : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shedule_StaffingTable_FOTId",
                table: "Shedule");

            migrationBuilder.DropIndex(
                name: "IX_Shedule_FOTId",
                table: "Shedule");

            migrationBuilder.DropColumn(
                name: "FOTId",
                table: "Shedule");

            migrationBuilder.CreateIndex(
                name: "IX_Shedule_StaffingTableId",
                table: "Shedule",
                column: "StaffingTableId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Shedule_StaffingTable_StaffingTableId",
                table: "Shedule",
                column: "StaffingTableId",
                principalTable: "StaffingTable",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shedule_StaffingTable_StaffingTableId",
                table: "Shedule");

            migrationBuilder.DropIndex(
                name: "IX_Shedule_StaffingTableId",
                table: "Shedule");

            migrationBuilder.AddColumn<int>(
                name: "FOTId",
                table: "Shedule",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Shedule_FOTId",
                table: "Shedule",
                column: "FOTId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Shedule_StaffingTable_FOTId",
                table: "Shedule",
                column: "FOTId",
                principalTable: "StaffingTable",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

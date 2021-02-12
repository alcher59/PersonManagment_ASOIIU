using Microsoft.EntityFrameworkCore.Migrations;

namespace PersonManagment.Data.Migrations
{
    public partial class fixStaffingTableVacation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StaffingTable_Vacations_vacationId",
                table: "StaffingTable");

            migrationBuilder.DropIndex(
                name: "IX_StaffingTable_vacationId",
                table: "StaffingTable");

            migrationBuilder.DropColumn(
                name: "vacationId",
                table: "StaffingTable");

            migrationBuilder.AddColumn<int>(
                name: "Vacationsid",
                table: "StaffingTable",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "vacationEntitlementId",
                table: "StaffingTable",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StaffingTable_Vacationsid",
                table: "StaffingTable",
                column: "Vacationsid");

            migrationBuilder.CreateIndex(
                name: "IX_StaffingTable_vacationEntitlementId",
                table: "StaffingTable",
                column: "vacationEntitlementId");

            migrationBuilder.AddForeignKey(
                name: "FK_StaffingTable_Vacations_Vacationsid",
                table: "StaffingTable",
                column: "Vacationsid",
                principalTable: "Vacations",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StaffingTable_VacationEntitlement_vacationEntitlementId",
                table: "StaffingTable",
                column: "vacationEntitlementId",
                principalTable: "VacationEntitlement",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StaffingTable_Vacations_Vacationsid",
                table: "StaffingTable");

            migrationBuilder.DropForeignKey(
                name: "FK_StaffingTable_VacationEntitlement_vacationEntitlementId",
                table: "StaffingTable");

            migrationBuilder.DropIndex(
                name: "IX_StaffingTable_Vacationsid",
                table: "StaffingTable");

            migrationBuilder.DropIndex(
                name: "IX_StaffingTable_vacationEntitlementId",
                table: "StaffingTable");

            migrationBuilder.DropColumn(
                name: "Vacationsid",
                table: "StaffingTable");

            migrationBuilder.DropColumn(
                name: "vacationEntitlementId",
                table: "StaffingTable");

            migrationBuilder.AddColumn<int>(
                name: "vacationId",
                table: "StaffingTable",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StaffingTable_vacationId",
                table: "StaffingTable",
                column: "vacationId");

            migrationBuilder.AddForeignKey(
                name: "FK_StaffingTable_Vacations_vacationId",
                table: "StaffingTable",
                column: "vacationId",
                principalTable: "Vacations",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

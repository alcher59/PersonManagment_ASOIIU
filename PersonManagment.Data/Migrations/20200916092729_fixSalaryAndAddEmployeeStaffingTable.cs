using Microsoft.EntityFrameworkCore.Migrations;

namespace PersonManagment.Data.Migrations
{
    public partial class fixSalaryAndAddEmployeeStaffingTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StaffingTable_Salary_salaryId",
                table: "StaffingTable");

            migrationBuilder.DropIndex(
                name: "IX_StaffingTable_salaryId",
                table: "StaffingTable");

            migrationBuilder.DropColumn(
                name: "salaryId",
                table: "StaffingTable");

            migrationBuilder.DropColumn(
                name: "Salary",
                table: "Employees");

            migrationBuilder.AddColumn<int>(
                name: "employeeId",
                table: "StaffingTable",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StaffingTable_employeeId",
                table: "StaffingTable",
                column: "employeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_StaffingTable_Employees_employeeId",
                table: "StaffingTable",
                column: "employeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StaffingTable_Employees_employeeId",
                table: "StaffingTable");

            migrationBuilder.DropIndex(
                name: "IX_StaffingTable_employeeId",
                table: "StaffingTable");

            migrationBuilder.DropColumn(
                name: "employeeId",
                table: "StaffingTable");

            migrationBuilder.AddColumn<int>(
                name: "salaryId",
                table: "StaffingTable",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Salary",
                table: "Employees",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateIndex(
                name: "IX_StaffingTable_salaryId",
                table: "StaffingTable",
                column: "salaryId");

            migrationBuilder.AddForeignKey(
                name: "FK_StaffingTable_Salary_salaryId",
                table: "StaffingTable",
                column: "salaryId",
                principalTable: "Salary",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

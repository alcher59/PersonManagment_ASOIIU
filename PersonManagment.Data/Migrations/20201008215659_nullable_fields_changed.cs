using Microsoft.EntityFrameworkCore.Migrations;

namespace PersonManagment.Data.Migrations
{
    public partial class nullable_fields_changed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recruitment_Positions_positionId",
                table: "Recruitment");

            migrationBuilder.DropForeignKey(
                name: "FK_Recruitment_Salary_salaryId",
                table: "Recruitment");

            migrationBuilder.DropForeignKey(
                name: "FK_Recruitment_TypeOfEmployment_typeOfEmploymentId",
                table: "Recruitment");

            migrationBuilder.DropForeignKey(
                name: "FK_Recruitment_Unit_unitId",
                table: "Recruitment");

            migrationBuilder.DropForeignKey(
                name: "FK_Recruitment_VacationEntitlement_vacationEntitlementId",
                table: "Recruitment");

            migrationBuilder.AlterColumn<int>(
                name: "vacationEntitlementId",
                table: "Recruitment",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "unitId",
                table: "Recruitment",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "typeOfEmploymentId",
                table: "Recruitment",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "salaryId",
                table: "Recruitment",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "positionId",
                table: "Recruitment",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "rate",
                table: "Contract",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AddForeignKey(
                name: "FK_Recruitment_Positions_positionId",
                table: "Recruitment",
                column: "positionId",
                principalTable: "Positions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Recruitment_Salary_salaryId",
                table: "Recruitment",
                column: "salaryId",
                principalTable: "Salary",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Recruitment_TypeOfEmployment_typeOfEmploymentId",
                table: "Recruitment",
                column: "typeOfEmploymentId",
                principalTable: "TypeOfEmployment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Recruitment_Unit_unitId",
                table: "Recruitment",
                column: "unitId",
                principalTable: "Unit",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Recruitment_VacationEntitlement_vacationEntitlementId",
                table: "Recruitment",
                column: "vacationEntitlementId",
                principalTable: "VacationEntitlement",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recruitment_Positions_positionId",
                table: "Recruitment");

            migrationBuilder.DropForeignKey(
                name: "FK_Recruitment_Salary_salaryId",
                table: "Recruitment");

            migrationBuilder.DropForeignKey(
                name: "FK_Recruitment_TypeOfEmployment_typeOfEmploymentId",
                table: "Recruitment");

            migrationBuilder.DropForeignKey(
                name: "FK_Recruitment_Unit_unitId",
                table: "Recruitment");

            migrationBuilder.DropForeignKey(
                name: "FK_Recruitment_VacationEntitlement_vacationEntitlementId",
                table: "Recruitment");

            migrationBuilder.AlterColumn<int>(
                name: "vacationEntitlementId",
                table: "Recruitment",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "unitId",
                table: "Recruitment",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "typeOfEmploymentId",
                table: "Recruitment",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "salaryId",
                table: "Recruitment",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "positionId",
                table: "Recruitment",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<double>(
                name: "rate",
                table: "Contract",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AddForeignKey(
                name: "FK_Recruitment_Positions_positionId",
                table: "Recruitment",
                column: "positionId",
                principalTable: "Positions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Recruitment_Salary_salaryId",
                table: "Recruitment",
                column: "salaryId",
                principalTable: "Salary",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Recruitment_TypeOfEmployment_typeOfEmploymentId",
                table: "Recruitment",
                column: "typeOfEmploymentId",
                principalTable: "TypeOfEmployment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Recruitment_Unit_unitId",
                table: "Recruitment",
                column: "unitId",
                principalTable: "Unit",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Recruitment_VacationEntitlement_vacationEntitlementId",
                table: "Recruitment",
                column: "vacationEntitlementId",
                principalTable: "VacationEntitlement",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

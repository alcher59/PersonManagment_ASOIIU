using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace PersonManagment.Data.Migrations
{
    public partial class addVacationType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "vacationTypeId",
                table: "VacationShedule",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "vacationTypeId",
                table: "Vacations",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "vacationTypeId",
                table: "StaffingTable",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "vacationTypeId",
                table: "Recruitment",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "VacationType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VacationType", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VacationShedule_vacationTypeId",
                table: "VacationShedule",
                column: "vacationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Vacations_vacationTypeId",
                table: "Vacations",
                column: "vacationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_StaffingTable_vacationTypeId",
                table: "StaffingTable",
                column: "vacationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Recruitment_vacationTypeId",
                table: "Recruitment",
                column: "vacationTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Recruitment_VacationType_vacationTypeId",
                table: "Recruitment",
                column: "vacationTypeId",
                principalTable: "VacationType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StaffingTable_VacationType_vacationTypeId",
                table: "StaffingTable",
                column: "vacationTypeId",
                principalTable: "VacationType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Vacations_VacationType_vacationTypeId",
                table: "Vacations",
                column: "vacationTypeId",
                principalTable: "VacationType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VacationShedule_VacationType_vacationTypeId",
                table: "VacationShedule",
                column: "vacationTypeId",
                principalTable: "VacationType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recruitment_VacationType_vacationTypeId",
                table: "Recruitment");

            migrationBuilder.DropForeignKey(
                name: "FK_StaffingTable_VacationType_vacationTypeId",
                table: "StaffingTable");

            migrationBuilder.DropForeignKey(
                name: "FK_Vacations_VacationType_vacationTypeId",
                table: "Vacations");

            migrationBuilder.DropForeignKey(
                name: "FK_VacationShedule_VacationType_vacationTypeId",
                table: "VacationShedule");

            migrationBuilder.DropTable(
                name: "VacationType");

            migrationBuilder.DropIndex(
                name: "IX_VacationShedule_vacationTypeId",
                table: "VacationShedule");

            migrationBuilder.DropIndex(
                name: "IX_Vacations_vacationTypeId",
                table: "Vacations");

            migrationBuilder.DropIndex(
                name: "IX_StaffingTable_vacationTypeId",
                table: "StaffingTable");

            migrationBuilder.DropIndex(
                name: "IX_Recruitment_vacationTypeId",
                table: "Recruitment");

            migrationBuilder.DropColumn(
                name: "vacationTypeId",
                table: "VacationShedule");

            migrationBuilder.DropColumn(
                name: "vacationTypeId",
                table: "Vacations");

            migrationBuilder.DropColumn(
                name: "vacationTypeId",
                table: "StaffingTable");

            migrationBuilder.DropColumn(
                name: "vacationTypeId",
                table: "Recruitment");
        }
    }
}

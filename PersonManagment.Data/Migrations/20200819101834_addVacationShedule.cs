using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace PersonManagment.Data.Migrations
{
    public partial class addVacationShedule : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VacationShedule",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    employeeId = table.Column<int>(nullable: true),
                    replacementEmployeeId = table.Column<int>(nullable: true),
                    dateStart = table.Column<int>(nullable: false),
                    dateEnd = table.Column<int>(nullable: false),
                    vacationEntitlementId = table.Column<int>(nullable: true),
                    vacationTransfer = table.Column<bool>(nullable: true),
                    causeTransferComment = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VacationShedule", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VacationShedule_Employees_employeeId",
                        column: x => x.employeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VacationShedule_Employees_replacementEmployeeId",
                        column: x => x.replacementEmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VacationShedule_VacationEntitlement_vacationEntitlementId",
                        column: x => x.vacationEntitlementId,
                        principalTable: "VacationEntitlement",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VacationShedule_employeeId",
                table: "VacationShedule",
                column: "employeeId");

            migrationBuilder.CreateIndex(
                name: "IX_VacationShedule_replacementEmployeeId",
                table: "VacationShedule",
                column: "replacementEmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_VacationShedule_vacationEntitlementId",
                table: "VacationShedule",
                column: "vacationEntitlementId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VacationShedule");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace PersonManagment.Data.Migrations
{
    public partial class migrationStaffingPositions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StaffingPositions",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    title = table.Column<string>(nullable: false),
                    unitId = table.Column<int>(nullable: false),
                    positionId = table.Column<int>(nullable: false),
                    sheduleId = table.Column<int>(nullable: false),
                    countRates = table.Column<int>(nullable: false),
                    acceptEmployeeId = table.Column<int>(nullable: true),
                    dateAccept = table.Column<int>(nullable: true),
                    daysVacation = table.Column<int>(nullable: false),
                    salaryId = table.Column<int>(nullable: false),
                    vacationId = table.Column<int>(nullable: false),
                    deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StaffingPositions", x => x.id);
                    table.ForeignKey(
                        name: "FK_StaffingPositions_Employees_acceptEmployeeId",
                        column: x => x.acceptEmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StaffingPositions_Positions_positionId",
                        column: x => x.positionId,
                        principalTable: "Positions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StaffingPositions_Salary_salaryId",
                        column: x => x.salaryId,
                        principalTable: "Salary",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StaffingPositions_Shedule_sheduleId",
                        column: x => x.sheduleId,
                        principalTable: "Shedule",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StaffingPositions_Unit_unitId",
                        column: x => x.unitId,
                        principalTable: "Unit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StaffingPositions_Vacations_vacationId",
                        column: x => x.vacationId,
                        principalTable: "Vacations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StaffingPositions_acceptEmployeeId",
                table: "StaffingPositions",
                column: "acceptEmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_StaffingPositions_positionId",
                table: "StaffingPositions",
                column: "positionId");

            migrationBuilder.CreateIndex(
                name: "IX_StaffingPositions_salaryId",
                table: "StaffingPositions",
                column: "salaryId");

            migrationBuilder.CreateIndex(
                name: "IX_StaffingPositions_sheduleId",
                table: "StaffingPositions",
                column: "sheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_StaffingPositions_unitId",
                table: "StaffingPositions",
                column: "unitId");

            migrationBuilder.CreateIndex(
                name: "IX_StaffingPositions_vacationId",
                table: "StaffingPositions",
                column: "vacationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StaffingPositions");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace PersonManagment.Data.Migrations
{
    public partial class changeStraffPositionLogic : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StaffingPositions");

            migrationBuilder.AddColumn<int>(
                name: "acceptEmployeeId",
                table: "StaffingTable",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "countRates",
                table: "StaffingTable",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "dateAccept",
                table: "StaffingTable",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "daysVacation",
                table: "StaffingTable",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "positionId",
                table: "StaffingTable",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "salaryId",
                table: "StaffingTable",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "title",
                table: "StaffingTable",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "unitId",
                table: "StaffingTable",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "vacationId",
                table: "StaffingTable",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StaffingTable_acceptEmployeeId",
                table: "StaffingTable",
                column: "acceptEmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_StaffingTable_positionId",
                table: "StaffingTable",
                column: "positionId");

            migrationBuilder.CreateIndex(
                name: "IX_StaffingTable_salaryId",
                table: "StaffingTable",
                column: "salaryId");

            migrationBuilder.CreateIndex(
                name: "IX_StaffingTable_unitId",
                table: "StaffingTable",
                column: "unitId");

            migrationBuilder.CreateIndex(
                name: "IX_StaffingTable_vacationId",
                table: "StaffingTable",
                column: "vacationId");

            migrationBuilder.AddForeignKey(
                name: "FK_StaffingTable_Employees_acceptEmployeeId",
                table: "StaffingTable",
                column: "acceptEmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StaffingTable_Positions_positionId",
                table: "StaffingTable",
                column: "positionId",
                principalTable: "Positions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StaffingTable_Salary_salaryId",
                table: "StaffingTable",
                column: "salaryId",
                principalTable: "Salary",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StaffingTable_Unit_unitId",
                table: "StaffingTable",
                column: "unitId",
                principalTable: "Unit",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StaffingTable_Vacations_vacationId",
                table: "StaffingTable",
                column: "vacationId",
                principalTable: "Vacations",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StaffingTable_Employees_acceptEmployeeId",
                table: "StaffingTable");

            migrationBuilder.DropForeignKey(
                name: "FK_StaffingTable_Positions_positionId",
                table: "StaffingTable");

            migrationBuilder.DropForeignKey(
                name: "FK_StaffingTable_Salary_salaryId",
                table: "StaffingTable");

            migrationBuilder.DropForeignKey(
                name: "FK_StaffingTable_Unit_unitId",
                table: "StaffingTable");

            migrationBuilder.DropForeignKey(
                name: "FK_StaffingTable_Vacations_vacationId",
                table: "StaffingTable");

            migrationBuilder.DropIndex(
                name: "IX_StaffingTable_acceptEmployeeId",
                table: "StaffingTable");

            migrationBuilder.DropIndex(
                name: "IX_StaffingTable_positionId",
                table: "StaffingTable");

            migrationBuilder.DropIndex(
                name: "IX_StaffingTable_salaryId",
                table: "StaffingTable");

            migrationBuilder.DropIndex(
                name: "IX_StaffingTable_unitId",
                table: "StaffingTable");

            migrationBuilder.DropIndex(
                name: "IX_StaffingTable_vacationId",
                table: "StaffingTable");

            migrationBuilder.DropColumn(
                name: "acceptEmployeeId",
                table: "StaffingTable");

            migrationBuilder.DropColumn(
                name: "countRates",
                table: "StaffingTable");

            migrationBuilder.DropColumn(
                name: "dateAccept",
                table: "StaffingTable");

            migrationBuilder.DropColumn(
                name: "daysVacation",
                table: "StaffingTable");

            migrationBuilder.DropColumn(
                name: "positionId",
                table: "StaffingTable");

            migrationBuilder.DropColumn(
                name: "salaryId",
                table: "StaffingTable");

            migrationBuilder.DropColumn(
                name: "title",
                table: "StaffingTable");

            migrationBuilder.DropColumn(
                name: "unitId",
                table: "StaffingTable");

            migrationBuilder.DropColumn(
                name: "vacationId",
                table: "StaffingTable");

            migrationBuilder.CreateTable(
                name: "StaffingPositions",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    acceptEmployeeId = table.Column<int>(type: "integer", nullable: true),
                    countRates = table.Column<int>(type: "integer", nullable: false),
                    dateAccept = table.Column<int>(type: "integer", nullable: true),
                    daysVacation = table.Column<int>(type: "integer", nullable: false),
                    deleted = table.Column<bool>(type: "boolean", nullable: false),
                    positionId = table.Column<int>(type: "integer", nullable: false),
                    salaryId = table.Column<int>(type: "integer", nullable: false),
                    sheduleId = table.Column<int>(type: "integer", nullable: false),
                    title = table.Column<string>(type: "text", nullable: false),
                    unitId = table.Column<int>(type: "integer", nullable: false),
                    vacationId = table.Column<int>(type: "integer", nullable: false)
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
    }
}

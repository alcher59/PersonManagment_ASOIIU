using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace PersonManagment.Data.Migrations
{
    public partial class addRecruitment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FOT",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    salary = table.Column<decimal>(nullable: true),
                    incentivePayments = table.Column<decimal>(nullable: true),
                    compensationPayments = table.Column<decimal>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FOT", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ReceptionConditions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    placeWork = table.Column<string>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceptionConditions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Salary",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    salary = table.Column<decimal>(nullable: false),
                    title = table.Column<string>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Salary", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VacationEntitlement",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    title = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VacationEntitlement", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StaffingTable",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FOTId = table.Column<int>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StaffingTable", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StaffingTable_FOT_FOTId",
                        column: x => x.FOTId,
                        principalTable: "FOT",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Contract",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    number = table.Column<string>(nullable: false),
                    dateStart = table.Column<int>(nullable: false),
                    dateEnd = table.Column<int>(nullable: false),
                    RCId = table.Column<int>(nullable: true),
                    OtherConditions = table.Column<string>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contract", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contract_ReceptionConditions_RCId",
                        column: x => x.RCId,
                        principalTable: "ReceptionConditions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Shedule",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    title = table.Column<string>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    StaffingTableId = table.Column<int>(nullable: true),
                    FOTId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shedule", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Shedule_StaffingTable_FOTId",
                        column: x => x.FOTId,
                        principalTable: "StaffingTable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Recruitment",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    employeeId = table.Column<int>(nullable: true),
                    dateOfReceipt = table.Column<int>(nullable: false),
                    positionId = table.Column<int>(nullable: true),
                    unitId = table.Column<int>(nullable: true),
                    sheduleId = table.Column<int>(nullable: true),
                    typeOfEmploymentId = table.Column<int>(nullable: true),
                    vacationEntitlementId = table.Column<int>(nullable: true),
                    vacationDays = table.Column<int>(nullable: false),
                    salarytId = table.Column<int>(nullable: true),
                    contractId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recruitment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Recruitment_Contract_contractId",
                        column: x => x.contractId,
                        principalTable: "Contract",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Recruitment_Employees_employeeId",
                        column: x => x.employeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Recruitment_Positions_positionId",
                        column: x => x.positionId,
                        principalTable: "Positions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Recruitment_Salary_salarytId",
                        column: x => x.salarytId,
                        principalTable: "Salary",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Recruitment_Shedule_sheduleId",
                        column: x => x.sheduleId,
                        principalTable: "Shedule",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Recruitment_TypeOfEmployment_typeOfEmploymentId",
                        column: x => x.typeOfEmploymentId,
                        principalTable: "TypeOfEmployment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Recruitment_Unit_unitId",
                        column: x => x.unitId,
                        principalTable: "Unit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Recruitment_VacationEntitlement_vacationEntitlementId",
                        column: x => x.vacationEntitlementId,
                        principalTable: "VacationEntitlement",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contract_RCId",
                table: "Contract",
                column: "RCId");

            migrationBuilder.CreateIndex(
                name: "IX_Recruitment_contractId",
                table: "Recruitment",
                column: "contractId");

            migrationBuilder.CreateIndex(
                name: "IX_Recruitment_employeeId",
                table: "Recruitment",
                column: "employeeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Recruitment_positionId",
                table: "Recruitment",
                column: "positionId");

            migrationBuilder.CreateIndex(
                name: "IX_Recruitment_salarytId",
                table: "Recruitment",
                column: "salarytId");

            migrationBuilder.CreateIndex(
                name: "IX_Recruitment_sheduleId",
                table: "Recruitment",
                column: "sheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_Recruitment_typeOfEmploymentId",
                table: "Recruitment",
                column: "typeOfEmploymentId");

            migrationBuilder.CreateIndex(
                name: "IX_Recruitment_unitId",
                table: "Recruitment",
                column: "unitId");

            migrationBuilder.CreateIndex(
                name: "IX_Recruitment_vacationEntitlementId",
                table: "Recruitment",
                column: "vacationEntitlementId");

            migrationBuilder.CreateIndex(
                name: "IX_Shedule_FOTId",
                table: "Shedule",
                column: "FOTId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StaffingTable_FOTId",
                table: "StaffingTable",
                column: "FOTId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Recruitment");

            migrationBuilder.DropTable(
                name: "Contract");

            migrationBuilder.DropTable(
                name: "Salary");

            migrationBuilder.DropTable(
                name: "Shedule");

            migrationBuilder.DropTable(
                name: "VacationEntitlement");

            migrationBuilder.DropTable(
                name: "ReceptionConditions");

            migrationBuilder.DropTable(
                name: "StaffingTable");

            migrationBuilder.DropTable(
                name: "FOT");
        }
    }
}

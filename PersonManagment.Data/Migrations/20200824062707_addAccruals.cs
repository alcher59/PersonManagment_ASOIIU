using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace PersonManagment.Data.Migrations
{
    public partial class addAccruals : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Accrualsid",
                table: "Employees",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DisablementIncapacityReason",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    title = table.Column<string>(nullable: false),
                    deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DisablementIncapacityReason", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "DocumentAccruals",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    title = table.Column<string>(nullable: false),
                    deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentAccruals", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "TypeAccrual",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    title = table.Column<string>(nullable: false),
                    deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeAccrual", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "TypeAward",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    title = table.Column<string>(nullable: false),
                    deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeAward", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Accruals",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    dateOfCreation = table.Column<int>(nullable: false),
                    number = table.Column<string>(nullable: false),
                    documentAccrualsId = table.Column<int>(nullable: false),
                    accrued = table.Column<decimal>(nullable: true),
                    withheld = table.Column<decimal>(nullable: true),
                    responsibleId = table.Column<int>(nullable: true),
                    comment = table.Column<string>(nullable: true),
                    deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accruals", x => x.id);
                    table.ForeignKey(
                        name: "FK_Accruals_DocumentAccruals_documentAccrualsId",
                        column: x => x.documentAccrualsId,
                        principalTable: "DocumentAccruals",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Accruals_Employees_responsibleId",
                        column: x => x.responsibleId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Awards",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    accrualId = table.Column<int>(nullable: false),
                    employeeId = table.Column<int>(nullable: false),
                    typeAwardId = table.Column<int>(nullable: false),
                    amount = table.Column<decimal>(nullable: false),
                    deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Awards", x => x.id);
                    table.ForeignKey(
                        name: "FK_Awards_Accruals_accrualId",
                        column: x => x.accrualId,
                        principalTable: "Accruals",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Awards_Employees_employeeId",
                        column: x => x.employeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Awards_TypeAward_typeAwardId",
                        column: x => x.typeAwardId,
                        principalTable: "TypeAward",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BusinessTrips",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    accrualId = table.Column<int>(nullable: false),
                    dateStart = table.Column<int>(nullable: false),
                    dateEnd = table.Column<int>(nullable: false),
                    deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessTrips", x => x.id);
                    table.ForeignKey(
                        name: "FK_BusinessTrips_Accruals_accrualId",
                        column: x => x.accrualId,
                        principalTable: "Accruals",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Payroll",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    accrualId = table.Column<int>(nullable: false),
                    employeeId = table.Column<int>(nullable: false),
                    typeAccrualId = table.Column<int>(nullable: false),
                    amount = table.Column<decimal>(nullable: false),
                    periodDateStart = table.Column<int>(nullable: false),
                    periodDateEnd = table.Column<int>(nullable: false),
                    cause = table.Column<string>(nullable: true),
                    deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payroll", x => x.id);
                    table.ForeignKey(
                        name: "FK_Payroll_Accruals_accrualId",
                        column: x => x.accrualId,
                        principalTable: "Accruals",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Payroll_Employees_employeeId",
                        column: x => x.employeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Payroll_TypeAccrual_typeAccrualId",
                        column: x => x.typeAccrualId,
                        principalTable: "TypeAccrual",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SickLeaves",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    accrualId = table.Column<int>(nullable: false),
                    disablementIncapacityReasonId = table.Column<int>(nullable: false),
                    dateStart = table.Column<int>(nullable: false),
                    ateEnd = table.Column<int>(nullable: false),
                    deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SickLeaves", x => x.id);
                    table.ForeignKey(
                        name: "FK_SickLeaves_Accruals_accrualId",
                        column: x => x.accrualId,
                        principalTable: "Accruals",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SickLeaves_DisablementIncapacityReason_disablementIncapacit~",
                        column: x => x.disablementIncapacityReasonId,
                        principalTable: "DisablementIncapacityReason",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Vacations",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    accrualId = table.Column<int>(nullable: false),
                    dateStart = table.Column<int>(nullable: false),
                    dateEnd = table.Column<int>(nullable: false),
                    vacationEntitlementId = table.Column<int>(nullable: false),
                    deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vacations", x => x.id);
                    table.ForeignKey(
                        name: "FK_Vacations_Accruals_accrualId",
                        column: x => x.accrualId,
                        principalTable: "Accruals",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Vacations_VacationEntitlement_vacationEntitlementId",
                        column: x => x.vacationEntitlementId,
                        principalTable: "VacationEntitlement",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_Accrualsid",
                table: "Employees",
                column: "Accrualsid");

            migrationBuilder.CreateIndex(
                name: "IX_Accruals_documentAccrualsId",
                table: "Accruals",
                column: "documentAccrualsId");

            migrationBuilder.CreateIndex(
                name: "IX_Accruals_responsibleId",
                table: "Accruals",
                column: "responsibleId");

            migrationBuilder.CreateIndex(
                name: "IX_Awards_accrualId",
                table: "Awards",
                column: "accrualId");

            migrationBuilder.CreateIndex(
                name: "IX_Awards_employeeId",
                table: "Awards",
                column: "employeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Awards_typeAwardId",
                table: "Awards",
                column: "typeAwardId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessTrips_accrualId",
                table: "BusinessTrips",
                column: "accrualId");

            migrationBuilder.CreateIndex(
                name: "IX_Payroll_accrualId",
                table: "Payroll",
                column: "accrualId");

            migrationBuilder.CreateIndex(
                name: "IX_Payroll_employeeId",
                table: "Payroll",
                column: "employeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Payroll_typeAccrualId",
                table: "Payroll",
                column: "typeAccrualId");

            migrationBuilder.CreateIndex(
                name: "IX_SickLeaves_accrualId",
                table: "SickLeaves",
                column: "accrualId");

            migrationBuilder.CreateIndex(
                name: "IX_SickLeaves_disablementIncapacityReasonId",
                table: "SickLeaves",
                column: "disablementIncapacityReasonId");

            migrationBuilder.CreateIndex(
                name: "IX_Vacations_accrualId",
                table: "Vacations",
                column: "accrualId");

            migrationBuilder.CreateIndex(
                name: "IX_Vacations_vacationEntitlementId",
                table: "Vacations",
                column: "vacationEntitlementId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Accruals_Accrualsid",
                table: "Employees",
                column: "Accrualsid",
                principalTable: "Accruals",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Accruals_Accrualsid",
                table: "Employees");

            migrationBuilder.DropTable(
                name: "Awards");

            migrationBuilder.DropTable(
                name: "BusinessTrips");

            migrationBuilder.DropTable(
                name: "Payroll");

            migrationBuilder.DropTable(
                name: "SickLeaves");

            migrationBuilder.DropTable(
                name: "Vacations");

            migrationBuilder.DropTable(
                name: "TypeAward");

            migrationBuilder.DropTable(
                name: "TypeAccrual");

            migrationBuilder.DropTable(
                name: "DisablementIncapacityReason");

            migrationBuilder.DropTable(
                name: "Accruals");

            migrationBuilder.DropTable(
                name: "DocumentAccruals");

            migrationBuilder.DropIndex(
                name: "IX_Employees_Accrualsid",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "Accrualsid",
                table: "Employees");
        }
    }
}

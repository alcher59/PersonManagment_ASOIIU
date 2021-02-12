using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace PersonManagment.Data.Migrations
{
    public partial class changelogicaccuralsemployee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Accruals_Accrualsid",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_Accrualsid",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "Accrualsid",
                table: "Employees");

            migrationBuilder.CreateTable(
                name: "AccrualsEmployee",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    employeeId = table.Column<int>(nullable: false),
                    accrualsId = table.Column<int>(nullable: false),
                    deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccrualsEmployee", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccrualsEmployee_Accruals_accrualsId",
                        column: x => x.accrualsId,
                        principalTable: "Accruals",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccrualsEmployee_Employees_employeeId",
                        column: x => x.employeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Indicators",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    title = table.Column<string>(nullable: true),
                    code = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Indicators", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "TimeSheet",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    employeeId = table.Column<int>(nullable: true),
                    date = table.Column<int>(nullable: false),
                    hours = table.Column<int>(nullable: false),
                    IndicatorsId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeSheet", x => x.id);
                    table.ForeignKey(
                        name: "FK_TimeSheet_Indicators_IndicatorsId",
                        column: x => x.IndicatorsId,
                        principalTable: "Indicators",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TimeSheet_Employees_employeeId",
                        column: x => x.employeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccrualsEmployee_accrualsId",
                table: "AccrualsEmployee",
                column: "accrualsId");

            migrationBuilder.CreateIndex(
                name: "IX_AccrualsEmployee_employeeId",
                table: "AccrualsEmployee",
                column: "employeeId");

            migrationBuilder.CreateIndex(
                name: "IX_TimeSheet_IndicatorsId",
                table: "TimeSheet",
                column: "IndicatorsId");

            migrationBuilder.CreateIndex(
                name: "IX_TimeSheet_employeeId",
                table: "TimeSheet",
                column: "employeeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccrualsEmployee");

            migrationBuilder.DropTable(
                name: "TimeSheet");

            migrationBuilder.DropTable(
                name: "Indicators");

            migrationBuilder.AddColumn<int>(
                name: "Accrualsid",
                table: "Employees",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_Accrualsid",
                table: "Employees",
                column: "Accrualsid");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Accruals_Accrualsid",
                table: "Employees",
                column: "Accrualsid",
                principalTable: "Accruals",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

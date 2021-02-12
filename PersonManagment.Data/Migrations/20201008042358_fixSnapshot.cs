using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace PersonManagment.Data.Migrations
{
    public partial class fixSnapshot : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "unitId",
                table: "StaffingTable",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "typeOfEmploymentId",
                table: "Recruitment",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "unitId",
                table: "Recruitment",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TypeOfEmployment",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeOfEmployment", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Unit",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Unit", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StaffingTable_unitId",
                table: "StaffingTable",
                column: "unitId");

            migrationBuilder.CreateIndex(
                name: "IX_Recruitment_typeOfEmploymentId",
                table: "Recruitment",
                column: "typeOfEmploymentId");

            migrationBuilder.CreateIndex(
                name: "IX_Recruitment_unitId",
                table: "Recruitment",
                column: "unitId");

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
                name: "FK_StaffingTable_Unit_unitId",
                table: "StaffingTable",
                column: "unitId",
                principalTable: "Unit",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recruitment_TypeOfEmployment_typeOfEmploymentId",
                table: "Recruitment");

            migrationBuilder.DropForeignKey(
                name: "FK_Recruitment_Unit_unitId",
                table: "Recruitment");

            migrationBuilder.DropForeignKey(
                name: "FK_StaffingTable_Unit_unitId",
                table: "StaffingTable");

            migrationBuilder.DropTable(
                name: "TypeOfEmployment");

            migrationBuilder.DropTable(
                name: "Unit");

            migrationBuilder.DropIndex(
                name: "IX_StaffingTable_unitId",
                table: "StaffingTable");

            migrationBuilder.DropIndex(
                name: "IX_Recruitment_typeOfEmploymentId",
                table: "Recruitment");

            migrationBuilder.DropIndex(
                name: "IX_Recruitment_unitId",
                table: "Recruitment");

            migrationBuilder.DropColumn(
                name: "unitId",
                table: "StaffingTable");

            migrationBuilder.DropColumn(
                name: "typeOfEmploymentId",
                table: "Recruitment");

            migrationBuilder.DropColumn(
                name: "unitId",
                table: "Recruitment");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace PersonManagment.Data.Migrations
{
    public partial class fixNamingAndAddDeleted : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recruitment_Salary_salarytId",
                table: "Recruitment");

            migrationBuilder.DropIndex(
                name: "IX_Recruitment_salarytId",
                table: "Recruitment");

            migrationBuilder.DropColumn(
                name: "salarytId",
                table: "Recruitment");

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "Recruitment",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "salaryId",
                table: "Recruitment",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Recruitment_salaryId",
                table: "Recruitment",
                column: "salaryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Recruitment_Salary_salaryId",
                table: "Recruitment",
                column: "salaryId",
                principalTable: "Salary",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recruitment_Salary_salaryId",
                table: "Recruitment");

            migrationBuilder.DropIndex(
                name: "IX_Recruitment_salaryId",
                table: "Recruitment");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Recruitment");

            migrationBuilder.DropColumn(
                name: "salaryId",
                table: "Recruitment");

            migrationBuilder.AddColumn<int>(
                name: "salarytId",
                table: "Recruitment",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Recruitment_salarytId",
                table: "Recruitment",
                column: "salarytId");

            migrationBuilder.AddForeignKey(
                name: "FK_Recruitment_Salary_salarytId",
                table: "Recruitment",
                column: "salarytId",
                principalTable: "Salary",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

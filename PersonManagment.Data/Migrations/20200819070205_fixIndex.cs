using Microsoft.EntityFrameworkCore.Migrations;

namespace PersonManagment.Data.Migrations
{
    public partial class fixIndex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Recruitment_employeeId",
                table: "Recruitment");

            migrationBuilder.CreateIndex(
                name: "IX_Recruitment_employeeId",
                table: "Recruitment",
                column: "employeeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Recruitment_employeeId",
                table: "Recruitment");

            migrationBuilder.CreateIndex(
                name: "IX_Recruitment_employeeId",
                table: "Recruitment",
                column: "employeeId",
                unique: true);
        }
    }
}

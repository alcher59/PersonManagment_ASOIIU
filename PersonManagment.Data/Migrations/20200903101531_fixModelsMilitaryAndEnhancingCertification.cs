using Microsoft.EntityFrameworkCore.Migrations;

namespace PersonManagment.Data.Migrations
{
    public partial class fixModelsMilitaryAndEnhancingCertification : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_EnhancingCertification_employeeId",
                table: "EnhancingCertification");

            migrationBuilder.CreateIndex(
                name: "IX_EnhancingCertification_employeeId",
                table: "EnhancingCertification",
                column: "employeeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_EnhancingCertification_employeeId",
                table: "EnhancingCertification");

            migrationBuilder.CreateIndex(
                name: "IX_EnhancingCertification_employeeId",
                table: "EnhancingCertification",
                column: "employeeId",
                unique: true);
        }
    }
}

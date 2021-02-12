using Microsoft.EntityFrameworkCore.Migrations;

namespace PersonManagment.Data.Migrations
{
    public partial class sheduleId_not_null : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recruitment_Shedule_sheduleId",
                table: "Recruitment");

            migrationBuilder.AlterColumn<int>(
                name: "sheduleId",
                table: "Recruitment",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Recruitment_Shedule_sheduleId",
                table: "Recruitment",
                column: "sheduleId",
                principalTable: "Shedule",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recruitment_Shedule_sheduleId",
                table: "Recruitment");

            migrationBuilder.AlterColumn<int>(
                name: "sheduleId",
                table: "Recruitment",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Recruitment_Shedule_sheduleId",
                table: "Recruitment",
                column: "sheduleId",
                principalTable: "Shedule",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

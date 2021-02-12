using Microsoft.EntityFrameworkCore.Migrations;

namespace PersonManagment.Data.Migrations
{
    public partial class addPasportNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonData_DocumentPassportData_DocumentPassportId",
                table: "PersonData");

            migrationBuilder.AlterColumn<int>(
                name: "DocumentPassportId",
                table: "PersonData",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonData_DocumentPassportData_DocumentPassportId",
                table: "PersonData",
                column: "DocumentPassportId",
                principalTable: "DocumentPassportData",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonData_DocumentPassportData_DocumentPassportId",
                table: "PersonData");

            migrationBuilder.AlterColumn<int>(
                name: "DocumentPassportId",
                table: "PersonData",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonData_DocumentPassportData_DocumentPassportId",
                table: "PersonData",
                column: "DocumentPassportId",
                principalTable: "DocumentPassportData",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace PersonManagment.Data.Migrations
{
    public partial class changedocdata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonData_DocumentPassportData_DocumentPassportId",
                table: "PersonData");

            migrationBuilder.DropIndex(
                name: "IX_PersonData_DocumentPassportId",
                table: "PersonData");

            migrationBuilder.DropColumn(
                name: "DocumentPassportId",
                table: "PersonData");

            migrationBuilder.AddColumn<int>(
                name: "countDocument",
                table: "PersonData",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "personDataId",
                table: "DocumentPassportData",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DocumentPassportData_personDataId",
                table: "DocumentPassportData",
                column: "personDataId");

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentPassportData_PersonData_personDataId",
                table: "DocumentPassportData",
                column: "personDataId",
                principalTable: "PersonData",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DocumentPassportData_PersonData_personDataId",
                table: "DocumentPassportData");

            migrationBuilder.DropIndex(
                name: "IX_DocumentPassportData_personDataId",
                table: "DocumentPassportData");

            migrationBuilder.DropColumn(
                name: "countDocument",
                table: "PersonData");

            migrationBuilder.DropColumn(
                name: "personDataId",
                table: "DocumentPassportData");

            migrationBuilder.AddColumn<int>(
                name: "DocumentPassportId",
                table: "PersonData",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PersonData_DocumentPassportId",
                table: "PersonData",
                column: "DocumentPassportId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonData_DocumentPassportData_DocumentPassportId",
                table: "PersonData",
                column: "DocumentPassportId",
                principalTable: "DocumentPassportData",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

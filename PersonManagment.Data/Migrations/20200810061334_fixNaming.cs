using Microsoft.EntityFrameworkCore.Migrations;

namespace PersonManagment.Data.Migrations
{
    public partial class fixNaming : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DategBirth",
                table: "PersonData");

            migrationBuilder.AddColumn<int>(
                name: "DateBirth",
                table: "PersonData",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateBirth",
                table: "PersonData");

            migrationBuilder.AddColumn<int>(
                name: "DategBirth",
                table: "PersonData",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}

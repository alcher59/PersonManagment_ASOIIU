using Microsoft.EntityFrameworkCore.Migrations;

namespace PersonManagment.Data.Migrations
{
    public partial class fixNameDateStartSickLeaves : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ateEnd",
                table: "SickLeaves");

            migrationBuilder.AddColumn<int>(
                name: "dateEnd",
                table: "SickLeaves",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "dateEnd",
                table: "SickLeaves");

            migrationBuilder.AddColumn<int>(
                name: "ateEnd",
                table: "SickLeaves",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace PersonManagment.Data.Migrations
{
    public partial class addTransfer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isTransfer",
                table: "Recruitment",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isTransfer",
                table: "Recruitment");
        }
    }
}

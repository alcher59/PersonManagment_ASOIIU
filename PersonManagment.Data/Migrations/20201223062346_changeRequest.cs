using Microsoft.EntityFrameworkCore.Migrations;

namespace PersonManagment.Data.Migrations
{
    public partial class changeRequest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "completed",
                table: "Request",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "createdDate",
                table: "Request",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "completed",
                table: "Request");

            migrationBuilder.DropColumn(
                name: "createdDate",
                table: "Request");
        }
    }
}

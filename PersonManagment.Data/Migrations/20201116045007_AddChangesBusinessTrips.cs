using Microsoft.EntityFrameworkCore.Migrations;

namespace PersonManagment.Data.Migrations
{
    public partial class AddChangesBusinessTrips : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "destination",
                table: "BusinessTrips",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "mission",
                table: "BusinessTrips",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "organization",
                table: "BusinessTrips",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "reason",
                table: "BusinessTrips",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "destination",
                table: "BusinessTrips");

            migrationBuilder.DropColumn(
                name: "mission",
                table: "BusinessTrips");

            migrationBuilder.DropColumn(
                name: "organization",
                table: "BusinessTrips");

            migrationBuilder.DropColumn(
                name: "reason",
                table: "BusinessTrips");
        }
    }
}

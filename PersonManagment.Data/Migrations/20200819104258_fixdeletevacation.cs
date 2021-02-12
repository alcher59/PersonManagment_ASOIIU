using Microsoft.EntityFrameworkCore.Migrations;

namespace PersonManagment.Data.Migrations
{
    public partial class fixdeletevacation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "vacationTransfer",
                table: "VacationShedule",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "deleted",
                table: "VacationShedule",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "deleted",
                table: "VacationShedule");

            migrationBuilder.AlterColumn<bool>(
                name: "vacationTransfer",
                table: "VacationShedule",
                type: "boolean",
                nullable: true,
                oldClrType: typeof(bool));
        }
    }
}

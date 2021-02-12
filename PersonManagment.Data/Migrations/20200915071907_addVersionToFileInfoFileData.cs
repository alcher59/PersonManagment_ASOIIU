using Microsoft.EntityFrameworkCore.Migrations;

namespace PersonManagment.Data.Migrations
{
    public partial class addVersionToFileInfoFileData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "version",
                table: "FilesInfoFilesData",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "version",
                table: "FilesInfoFilesData");
        }
    }
}

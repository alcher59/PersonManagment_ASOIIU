using Microsoft.EntityFrameworkCore.Migrations;

namespace PersonManagment.Data.Migrations
{
    public partial class fixTableExperienceWork : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExperienceWork_Experience_experiencenId",
                table: "ExperienceWork");

            migrationBuilder.DropIndex(
                name: "IX_ExperienceWork_experiencenId",
                table: "ExperienceWork");

            migrationBuilder.DropColumn(
                name: "experiencenId",
                table: "ExperienceWork");

            migrationBuilder.AddColumn<int>(
                name: "experienceId",
                table: "ExperienceWork",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ExperienceWork_experienceId",
                table: "ExperienceWork",
                column: "experienceId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExperienceWork_Experience_experienceId",
                table: "ExperienceWork",
                column: "experienceId",
                principalTable: "Experience",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExperienceWork_Experience_experienceId",
                table: "ExperienceWork");

            migrationBuilder.DropIndex(
                name: "IX_ExperienceWork_experienceId",
                table: "ExperienceWork");

            migrationBuilder.DropColumn(
                name: "experienceId",
                table: "ExperienceWork");

            migrationBuilder.AddColumn<int>(
                name: "experiencenId",
                table: "ExperienceWork",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ExperienceWork_experiencenId",
                table: "ExperienceWork",
                column: "experiencenId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExperienceWork_Experience_experiencenId",
                table: "ExperienceWork",
                column: "experiencenId",
                principalTable: "Experience",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

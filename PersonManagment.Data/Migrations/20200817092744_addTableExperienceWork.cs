using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace PersonManagment.Data.Migrations
{
    public partial class addTableExperienceWork : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "dateEnd",
                table: "Experience");

            migrationBuilder.DropColumn(
                name: "dateStart",
                table: "Experience");

            migrationBuilder.DropColumn(
                name: "placeWork",
                table: "Experience");

            migrationBuilder.AddColumn<int>(
                name: "employeeId",
                table: "Experience",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "WorkPlace",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    title = table.Column<string>(nullable: false),
                    deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkPlace", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "ExperienceWork",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    experiencenId = table.Column<int>(nullable: false),
                    workPlaceId = table.Column<int>(nullable: false),
                    dateStart = table.Column<int>(nullable: true),
                    dateEnd = table.Column<int>(nullable: true),
                    deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExperienceWork", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExperienceWork_Experience_experiencenId",
                        column: x => x.experiencenId,
                        principalTable: "Experience",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExperienceWork_WorkPlace_workPlaceId",
                        column: x => x.workPlaceId,
                        principalTable: "WorkPlace",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Experience_employeeId",
                table: "Experience",
                column: "employeeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExperienceWork_experiencenId",
                table: "ExperienceWork",
                column: "experiencenId");

            migrationBuilder.CreateIndex(
                name: "IX_ExperienceWork_workPlaceId",
                table: "ExperienceWork",
                column: "workPlaceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Experience_Employees_employeeId",
                table: "Experience",
                column: "employeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Experience_Employees_employeeId",
                table: "Experience");

            migrationBuilder.DropTable(
                name: "ExperienceWork");

            migrationBuilder.DropTable(
                name: "WorkPlace");

            migrationBuilder.DropIndex(
                name: "IX_Experience_employeeId",
                table: "Experience");

            migrationBuilder.DropColumn(
                name: "employeeId",
                table: "Experience");

            migrationBuilder.AddColumn<int>(
                name: "dateEnd",
                table: "Experience",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "dateStart",
                table: "Experience",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "placeWork",
                table: "Experience",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}

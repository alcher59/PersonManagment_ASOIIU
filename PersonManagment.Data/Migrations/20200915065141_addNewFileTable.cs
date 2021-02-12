using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace PersonManagment.Data.Migrations
{
    public partial class addNewFileTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FilesData_Files_filesId",
                table: "FilesData");

            migrationBuilder.DropTable(
                name: "Files");

            migrationBuilder.DropIndex(
                name: "IX_FilesData_filesId",
                table: "FilesData");

            migrationBuilder.DropColumn(
                name: "filesId",
                table: "FilesData");

            migrationBuilder.CreateTable(
                name: "FilesInfo",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    title = table.Column<string>(nullable: true),
                    comment = table.Column<string>(nullable: true),
                    deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilesInfo", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "FilesInfoFilesData",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    filesInfoId = table.Column<int>(nullable: false),
                    filesDataId = table.Column<int>(nullable: false),
                    date = table.Column<int>(nullable: false),
                    isActual = table.Column<bool>(nullable: false),
                    deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilesInfoFilesData", x => x.id);
                    table.ForeignKey(
                        name: "FK_FilesInfoFilesData_FilesData_filesDataId",
                        column: x => x.filesDataId,
                        principalTable: "FilesData",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FilesInfoFilesData_FilesInfo_filesInfoId",
                        column: x => x.filesInfoId,
                        principalTable: "FilesInfo",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FilesInfoFilesData_filesDataId",
                table: "FilesInfoFilesData",
                column: "filesDataId");

            migrationBuilder.CreateIndex(
                name: "IX_FilesInfoFilesData_filesInfoId",
                table: "FilesInfoFilesData",
                column: "filesInfoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FilesInfoFilesData");

            migrationBuilder.DropTable(
                name: "FilesInfo");

            migrationBuilder.AddColumn<int>(
                name: "filesId",
                table: "FilesData",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Files",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    comment = table.Column<string>(type: "text", nullable: true),
                    date = table.Column<int>(type: "integer", nullable: false),
                    deleted = table.Column<bool>(type: "boolean", nullable: false),
                    title = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Files", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FilesData_filesId",
                table: "FilesData",
                column: "filesId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_FilesData_Files_filesId",
                table: "FilesData",
                column: "filesId",
                principalTable: "Files",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

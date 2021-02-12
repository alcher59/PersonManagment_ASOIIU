using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace PersonManagment.Data.Migrations
{
    public partial class addModelsMilitaryAndEnhancingCertification : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EnhancingCertification",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    employeeId = table.Column<int>(nullable: true),
                    date = table.Column<int>(nullable: false),
                    solve = table.Column<string>(nullable: true),
                    number = table.Column<int>(nullable: false),
                    dateDocument = table.Column<int>(nullable: false),
                    reason = table.Column<string>(nullable: true),
                    deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnhancingCertification", x => x.id);
                    table.ForeignKey(
                        name: "FK_EnhancingCertification_Employees_employeeId",
                        column: x => x.employeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MilitaryFitnessCategory",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    title = table.Column<string>(nullable: false),
                    deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MilitaryFitnessCategory", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "MilitaryProfile",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    title = table.Column<string>(nullable: false),
                    deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MilitaryProfile", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "MilitaryRank",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    title = table.Column<string>(nullable: false),
                    deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MilitaryRank", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "StockCategory",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    title = table.Column<string>(nullable: false),
                    deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockCategory", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "TypeMilitaryRegistration",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    title = table.Column<string>(nullable: false),
                    deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeMilitaryRegistration", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "MilitaryRegistration",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    employeeId = table.Column<int>(nullable: true),
                    stockCategoryId = table.Column<int>(nullable: false),
                    militaryRankId = table.Column<int>(nullable: false),
                    militaryProfileId = table.Column<int>(nullable: false),
                    VUS = table.Column<string>(nullable: true),
                    militaryFitnessCategoryId = table.Column<int>(nullable: false),
                    nameOfCommissariat = table.Column<string>(nullable: true),
                    typeMilitaryRegistrationId = table.Column<int>(nullable: false),
                    deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MilitaryRegistration", x => x.id);
                    table.ForeignKey(
                        name: "FK_MilitaryRegistration_Employees_employeeId",
                        column: x => x.employeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MilitaryRegistration_MilitaryFitnessCategory_militaryFitnes~",
                        column: x => x.militaryFitnessCategoryId,
                        principalTable: "MilitaryFitnessCategory",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MilitaryRegistration_MilitaryProfile_militaryProfileId",
                        column: x => x.militaryProfileId,
                        principalTable: "MilitaryProfile",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MilitaryRegistration_MilitaryRank_militaryRankId",
                        column: x => x.militaryRankId,
                        principalTable: "MilitaryRank",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MilitaryRegistration_StockCategory_stockCategoryId",
                        column: x => x.stockCategoryId,
                        principalTable: "StockCategory",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MilitaryRegistration_TypeMilitaryRegistration_typeMilitaryR~",
                        column: x => x.typeMilitaryRegistrationId,
                        principalTable: "TypeMilitaryRegistration",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EnhancingCertification_employeeId",
                table: "EnhancingCertification",
                column: "employeeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MilitaryRegistration_employeeId",
                table: "MilitaryRegistration",
                column: "employeeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MilitaryRegistration_militaryFitnessCategoryId",
                table: "MilitaryRegistration",
                column: "militaryFitnessCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_MilitaryRegistration_militaryProfileId",
                table: "MilitaryRegistration",
                column: "militaryProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_MilitaryRegistration_militaryRankId",
                table: "MilitaryRegistration",
                column: "militaryRankId");

            migrationBuilder.CreateIndex(
                name: "IX_MilitaryRegistration_stockCategoryId",
                table: "MilitaryRegistration",
                column: "stockCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_MilitaryRegistration_typeMilitaryRegistrationId",
                table: "MilitaryRegistration",
                column: "typeMilitaryRegistrationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EnhancingCertification");

            migrationBuilder.DropTable(
                name: "MilitaryRegistration");

            migrationBuilder.DropTable(
                name: "MilitaryFitnessCategory");

            migrationBuilder.DropTable(
                name: "MilitaryProfile");

            migrationBuilder.DropTable(
                name: "MilitaryRank");

            migrationBuilder.DropTable(
                name: "StockCategory");

            migrationBuilder.DropTable(
                name: "TypeMilitaryRegistration");
        }
    }
}

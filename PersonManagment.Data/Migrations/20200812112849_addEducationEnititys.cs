using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace PersonManagment.Data.Migrations
{
    public partial class addEducationEnititys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AcademicDegrees",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    title = table.Column<string>(nullable: false),
                    deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AcademicDegrees", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "AcademicTitles",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    title = table.Column<string>(nullable: false),
                    deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AcademicTitles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "DiplomaDocument",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    serial = table.Column<int>(nullable: false),
                    number = table.Column<int>(nullable: false),
                    deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiplomaDocument", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "EducationalInstitution",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    title = table.Column<string>(nullable: false),
                    deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EducationalInstitution", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Qualification",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    title = table.Column<string>(nullable: false),
                    deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Qualification", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Specialty",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    title = table.Column<string>(nullable: false),
                    deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Specialty", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "TypeOfEducation",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    title = table.Column<string>(nullable: false),
                    deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeOfEducation", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Education",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    employeeId = table.Column<int>(nullable: true),
                    typeOfEducationId = table.Column<int>(nullable: true),
                    educationalInstitutionId = table.Column<int>(nullable: true),
                    dateStartEducation = table.Column<int>(nullable: true),
                    dateEndEducation = table.Column<int>(nullable: true),
                    specialtyId = table.Column<int>(nullable: true),
                    qualificationId = table.Column<int>(nullable: true),
                    documentTypeId = table.Column<int>(nullable: true),
                    diplomaDocumentId = table.Column<int>(nullable: true),
                    scientificWorks = table.Column<bool>(nullable: false),
                    inventions = table.Column<bool>(nullable: false),
                    deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Education", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Education_DiplomaDocument_diplomaDocumentId",
                        column: x => x.diplomaDocumentId,
                        principalTable: "DiplomaDocument",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Education_DocumentType_documentTypeId",
                        column: x => x.documentTypeId,
                        principalTable: "DocumentType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Education_EducationalInstitution_educationalInstitutionId",
                        column: x => x.educationalInstitutionId,
                        principalTable: "EducationalInstitution",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Education_Employees_employeeId",
                        column: x => x.employeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Education_Qualification_qualificationId",
                        column: x => x.qualificationId,
                        principalTable: "Qualification",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Education_Specialty_specialtyId",
                        column: x => x.specialtyId,
                        principalTable: "Specialty",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Education_TypeOfEducation_typeOfEducationId",
                        column: x => x.typeOfEducationId,
                        principalTable: "TypeOfEducation",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EducationDegrees",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    educationId = table.Column<int>(nullable: false),
                    academicDegreesId = table.Column<int>(nullable: false),
                    deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EducationDegrees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EducationDegrees_AcademicDegrees_academicDegreesId",
                        column: x => x.academicDegreesId,
                        principalTable: "AcademicDegrees",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EducationDegrees_Education_educationId",
                        column: x => x.educationId,
                        principalTable: "Education",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EducationTitles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    educationId = table.Column<int>(nullable: false),
                    academicTitlesId = table.Column<int>(nullable: false),
                    deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EducationTitles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EducationTitles_AcademicTitles_academicTitlesId",
                        column: x => x.academicTitlesId,
                        principalTable: "AcademicTitles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EducationTitles_Education_educationId",
                        column: x => x.educationId,
                        principalTable: "Education",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Education_diplomaDocumentId",
                table: "Education",
                column: "diplomaDocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_Education_documentTypeId",
                table: "Education",
                column: "documentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Education_educationalInstitutionId",
                table: "Education",
                column: "educationalInstitutionId");

            migrationBuilder.CreateIndex(
                name: "IX_Education_employeeId",
                table: "Education",
                column: "employeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Education_qualificationId",
                table: "Education",
                column: "qualificationId");

            migrationBuilder.CreateIndex(
                name: "IX_Education_specialtyId",
                table: "Education",
                column: "specialtyId");

            migrationBuilder.CreateIndex(
                name: "IX_Education_typeOfEducationId",
                table: "Education",
                column: "typeOfEducationId");

            migrationBuilder.CreateIndex(
                name: "IX_EducationDegrees_academicDegreesId",
                table: "EducationDegrees",
                column: "academicDegreesId");

            migrationBuilder.CreateIndex(
                name: "IX_EducationDegrees_educationId",
                table: "EducationDegrees",
                column: "educationId");

            migrationBuilder.CreateIndex(
                name: "IX_EducationTitles_academicTitlesId",
                table: "EducationTitles",
                column: "academicTitlesId");

            migrationBuilder.CreateIndex(
                name: "IX_EducationTitles_educationId",
                table: "EducationTitles",
                column: "educationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EducationDegrees");

            migrationBuilder.DropTable(
                name: "EducationTitles");

            migrationBuilder.DropTable(
                name: "AcademicDegrees");

            migrationBuilder.DropTable(
                name: "AcademicTitles");

            migrationBuilder.DropTable(
                name: "Education");

            migrationBuilder.DropTable(
                name: "DiplomaDocument");

            migrationBuilder.DropTable(
                name: "EducationalInstitution");

            migrationBuilder.DropTable(
                name: "Qualification");

            migrationBuilder.DropTable(
                name: "Specialty");

            migrationBuilder.DropTable(
                name: "TypeOfEducation");
        }
    }
}

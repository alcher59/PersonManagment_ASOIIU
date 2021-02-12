using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace PersonManagment.Data.Migrations
{
    public partial class addPersonDataAndDependency : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Country",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DocumentType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PersonAddress",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RegistrationAddress = table.Column<string>(nullable: true),
                    RegistrationDate = table.Column<int>(nullable: false),
                    ResidenceAddress = table.Column<string>(nullable: true),
                    OutsideAddress = table.Column<string>(nullable: true),
                    InformationAddress = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonAddress", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PersonContacts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PhoneNumber = table.Column<string>(nullable: true),
                    HomePhoneNumber = table.Column<string>(nullable: true),
                    WorkPhoneNumber = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonContacts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DocumentPassportData",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CountryId = table.Column<int>(nullable: false),
                    Series = table.Column<int>(nullable: false),
                    Number = table.Column<int>(nullable: false),
                    DocumentIssued = table.Column<string>(nullable: true),
                    IssuedDate = table.Column<int>(nullable: false),
                    Code = table.Column<int>(nullable: false),
                    Validity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentPassportData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DocumentPassportData_Country_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonData",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Birthplace = table.Column<string>(nullable: false),
                    INN = table.Column<long>(nullable: false),
                    InformationСitizenshipDateStart = table.Column<int>(nullable: false),
                    DocumentTypeId = table.Column<int>(nullable: false),
                    DocumentPassportId = table.Column<int>(nullable: false),
                    ValidityDocumentDateStart = table.Column<int>(nullable: false),
                    PersonContactsId = table.Column<int>(nullable: false),
                    PersonAddressId = table.Column<int>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    EmployeeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonData_DocumentPassportData_DocumentPassportId",
                        column: x => x.DocumentPassportId,
                        principalTable: "DocumentPassportData",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonData_DocumentType_DocumentTypeId",
                        column: x => x.DocumentTypeId,
                        principalTable: "DocumentType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonData_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonData_PersonAddress_PersonAddressId",
                        column: x => x.PersonAddressId,
                        principalTable: "PersonAddress",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonData_PersonContacts_PersonContactsId",
                        column: x => x.PersonContactsId,
                        principalTable: "PersonContacts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DocumentPassportData_CountryId",
                table: "DocumentPassportData",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonData_DocumentPassportId",
                table: "PersonData",
                column: "DocumentPassportId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PersonData_DocumentTypeId",
                table: "PersonData",
                column: "DocumentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonData_EmployeeId",
                table: "PersonData",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonData_PersonAddressId",
                table: "PersonData",
                column: "PersonAddressId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PersonData_PersonContactsId",
                table: "PersonData",
                column: "PersonContactsId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PersonData");

            migrationBuilder.DropTable(
                name: "DocumentPassportData");

            migrationBuilder.DropTable(
                name: "DocumentType");

            migrationBuilder.DropTable(
                name: "PersonAddress");

            migrationBuilder.DropTable(
                name: "PersonContacts");

            migrationBuilder.DropTable(
                name: "Country");
        }
    }
}

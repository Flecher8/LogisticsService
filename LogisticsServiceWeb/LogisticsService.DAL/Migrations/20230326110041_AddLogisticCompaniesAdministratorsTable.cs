using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogisticsService.DAL.Migrations
{
    public partial class AddLogisticCompaniesAdministratorsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LogisticCompaniesAdministrators",
                columns: table => new
                {
                    LogisticCompaniesAdministratorsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HashedPassword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LogisticCompanyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogisticCompaniesAdministrators", x => x.LogisticCompaniesAdministratorsId);
                    table.ForeignKey(
                        name: "FK_LogisticCompaniesAdministrators_LogisticCompanies_LogisticCompanyId",
                        column: x => x.LogisticCompanyId,
                        principalTable: "LogisticCompanies",
                        principalColumn: "LogisticCompanyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LogisticCompaniesAdministrators_LogisticCompanyId",
                table: "LogisticCompaniesAdministrators",
                column: "LogisticCompanyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LogisticCompaniesAdministrators");
        }
    }
}

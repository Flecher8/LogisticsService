using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogisticsService.DAL.Migrations
{
    public partial class ChangedFieldNameInCompanies : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CompanyEmail",
                table: "PrivateCompanies",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "LogisticCompanyEmail",
                table: "LogisticCompanies",
                newName: "Email");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Email",
                table: "PrivateCompanies",
                newName: "CompanyEmail");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "LogisticCompanies",
                newName: "LogisticCompanyEmail");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogisticsService.DAL.Migrations
{
    public partial class ChangedFieldCompanyNameInLogisticCompaniesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LogisticCompanyName",
                table: "LogisticCompanies",
                newName: "CompanyName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CompanyName",
                table: "LogisticCompanies",
                newName: "LogisticCompanyName");
        }
    }
}

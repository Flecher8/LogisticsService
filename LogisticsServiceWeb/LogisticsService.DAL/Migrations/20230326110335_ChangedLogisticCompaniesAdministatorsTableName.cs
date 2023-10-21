using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogisticsService.DAL.Migrations
{
    public partial class ChangedLogisticCompaniesAdministatorsTableName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LogisticCompaniesAdministratorsId",
                table: "LogisticCompaniesAdministrators",
                newName: "LogisticCompaniesAdministratorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LogisticCompaniesAdministratorId",
                table: "LogisticCompaniesAdministrators",
                newName: "LogisticCompaniesAdministratorsId");
        }
    }
}

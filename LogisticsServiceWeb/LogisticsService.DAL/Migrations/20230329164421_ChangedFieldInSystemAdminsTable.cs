using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogisticsService.DAL.Migrations
{
    public partial class ChangedFieldInSystemAdminsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FistName",
                table: "SystemAdmins",
                newName: "FirstName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "SystemAdmins",
                newName: "FistName");
        }
    }
}

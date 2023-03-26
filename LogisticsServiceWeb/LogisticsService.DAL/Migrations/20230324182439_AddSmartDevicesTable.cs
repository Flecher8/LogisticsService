using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogisticsService.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddSmartDevicesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SmartDevices",
                columns: table => new
                {
                    SmartDeviceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumberOfSensors = table.Column<int>(type: "int", nullable: false),
                    LogisticCompanyId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SmartDevices", x => x.SmartDeviceId);
                    table.ForeignKey(
                        name: "FK_SmartDevices_LogisticCompanies_LogisticCompanyId",
                        column: x => x.LogisticCompanyId,
                        principalTable: "LogisticCompanies",
                        principalColumn: "LogisticCompanyId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_SmartDevices_LogisticCompanyId",
                table: "SmartDevices",
                column: "LogisticCompanyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SmartDevices");
        }
    }
}

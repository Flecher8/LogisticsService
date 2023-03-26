using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogisticsService.DAL.Migrations
{
    public partial class AddOrdersTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Price",
                table: "SubscriptionTypes",
                type: "float",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "CompanyName",
                table: "PrivateCompanies",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Width",
                table: "Cargos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PrivateCompanyId = table.Column<int>(type: "int", nullable: false),
                    LogisticCompanyId = table.Column<int>(type: "int", nullable: false),
                    LogisticCompaniesDriverId = table.Column<int>(type: "int", nullable: false),
                    SensorId = table.Column<int>(type: "int", nullable: false),
                    CargoId = table.Column<int>(type: "int", nullable: false),
                    OrderStatus = table.Column<int>(type: "int", nullable: false),
                    CreationDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EstimatedDeliveryDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartDeliveryDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeliveryDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    PathLength = table.Column<double>(type: "float", nullable: false),
                    DeliveryStartAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeliveryEndAddress = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_Orders_Cargos_CargoId",
                        column: x => x.CargoId,
                        principalTable: "Cargos",
                        principalColumn: "CargoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_LogisticCompanies_LogisticCompanyId",
                        column: x => x.LogisticCompanyId,
                        principalTable: "LogisticCompanies",
                        principalColumn: "LogisticCompanyId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Orders_LogisticCompaniesDrivers_LogisticCompaniesDriverId",
                        column: x => x.LogisticCompaniesDriverId,
                        principalTable: "LogisticCompaniesDrivers",
                        principalColumn: "LogisticCompaniesDriverId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Orders_PrivateCompanies_PrivateCompanyId",
                        column: x => x.PrivateCompanyId,
                        principalTable: "PrivateCompanies",
                        principalColumn: "PrivateCompanyId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Orders_Sensors_SensorId",
                        column: x => x.SensorId,
                        principalTable: "Sensors",
                        principalColumn: "SensorId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CargoId",
                table: "Orders",
                column: "CargoId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_LogisticCompaniesDriverId",
                table: "Orders",
                column: "LogisticCompaniesDriverId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_LogisticCompanyId",
                table: "Orders",
                column: "LogisticCompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_PrivateCompanyId",
                table: "Orders",
                column: "PrivateCompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_SensorId",
                table: "Orders",
                column: "SensorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropColumn(
                name: "Width",
                table: "Cargos");

            migrationBuilder.AlterColumn<int>(
                name: "Price",
                table: "SubscriptionTypes",
                type: "int",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<string>(
                name: "CompanyName",
                table: "PrivateCompanies",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}

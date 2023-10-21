using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogisticsService.DAL.Migrations
{
    public partial class AddCancelledOrdersTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Cargos_CargoId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_LogisticCompanies_LogisticCompanyId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_LogisticCompaniesDrivers_LogisticCompaniesDriverId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_PrivateCompanies_PrivateCompanyId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Sensors_SensorId",
                table: "Orders");

            migrationBuilder.AlterColumn<int>(
                name: "SensorId",
                table: "Orders",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "PrivateCompanyId",
                table: "Orders",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "LogisticCompanyId",
                table: "Orders",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "LogisticCompaniesDriverId",
                table: "Orders",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CargoId",
                table: "Orders",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "CancelledOrders",
                columns: table => new
                {
                    CancelledOrderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CancelledBy = table.Column<int>(type: "int", nullable: false),
                    CancelledById = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CancelledOrders", x => x.CancelledOrderId);
                    table.ForeignKey(
                        name: "FK_CancelledOrders_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CancelledOrders_OrderId",
                table: "CancelledOrders",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Cargos_CargoId",
                table: "Orders",
                column: "CargoId",
                principalTable: "Cargos",
                principalColumn: "CargoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_LogisticCompanies_LogisticCompanyId",
                table: "Orders",
                column: "LogisticCompanyId",
                principalTable: "LogisticCompanies",
                principalColumn: "LogisticCompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_LogisticCompaniesDrivers_LogisticCompaniesDriverId",
                table: "Orders",
                column: "LogisticCompaniesDriverId",
                principalTable: "LogisticCompaniesDrivers",
                principalColumn: "LogisticCompaniesDriverId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_PrivateCompanies_PrivateCompanyId",
                table: "Orders",
                column: "PrivateCompanyId",
                principalTable: "PrivateCompanies",
                principalColumn: "PrivateCompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Sensors_SensorId",
                table: "Orders",
                column: "SensorId",
                principalTable: "Sensors",
                principalColumn: "SensorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Cargos_CargoId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_LogisticCompanies_LogisticCompanyId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_LogisticCompaniesDrivers_LogisticCompaniesDriverId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_PrivateCompanies_PrivateCompanyId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Sensors_SensorId",
                table: "Orders");

            migrationBuilder.DropTable(
                name: "CancelledOrders");

            migrationBuilder.AlterColumn<int>(
                name: "SensorId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PrivateCompanyId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "LogisticCompanyId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "LogisticCompaniesDriverId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CargoId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Cargos_CargoId",
                table: "Orders",
                column: "CargoId",
                principalTable: "Cargos",
                principalColumn: "CargoId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_LogisticCompanies_LogisticCompanyId",
                table: "Orders",
                column: "LogisticCompanyId",
                principalTable: "LogisticCompanies",
                principalColumn: "LogisticCompanyId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_LogisticCompaniesDrivers_LogisticCompaniesDriverId",
                table: "Orders",
                column: "LogisticCompaniesDriverId",
                principalTable: "LogisticCompaniesDrivers",
                principalColumn: "LogisticCompaniesDriverId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_PrivateCompanies_PrivateCompanyId",
                table: "Orders",
                column: "PrivateCompanyId",
                principalTable: "PrivateCompanies",
                principalColumn: "PrivateCompanyId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Sensors_SensorId",
                table: "Orders",
                column: "SensorId",
                principalTable: "Sensors",
                principalColumn: "SensorId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

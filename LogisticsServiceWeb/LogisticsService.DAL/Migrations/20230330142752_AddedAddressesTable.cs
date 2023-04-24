using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogisticsService.DAL.Migrations
{
    public partial class AddedAddressesTable : Migration
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
                name: "FK_Orders_PrivateCompanies_PrivateCompanyId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "DeliveryEndAddress",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "DeliveryStartAddress",
                table: "Orders");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDeliveryDateTime",
                table: "Orders",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

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

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeliveryDateTime",
                table: "Orders",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<int>(
                name: "CargoId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EndDeliveryAddressAddressId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StartDeliveryAddressAddressId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    AddressId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AddressName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Latitude = table.Column<double>(type: "float", nullable: false),
                    Longitute = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.AddressId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_EndDeliveryAddressAddressId",
                table: "Orders",
                column: "EndDeliveryAddressAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_StartDeliveryAddressAddressId",
                table: "Orders",
                column: "StartDeliveryAddressAddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Addresses_EndDeliveryAddressAddressId",
                table: "Orders",
                column: "EndDeliveryAddressAddressId",
                principalTable: "Addresses",
                principalColumn: "AddressId",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Addresses_StartDeliveryAddressAddressId",
                table: "Orders",
                column: "StartDeliveryAddressAddressId",
                principalTable: "Addresses",
                principalColumn: "AddressId",
                onDelete: ReferentialAction.NoAction);

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
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_PrivateCompanies_PrivateCompanyId",
                table: "Orders",
                column: "PrivateCompanyId",
                principalTable: "PrivateCompanies",
                principalColumn: "PrivateCompanyId",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Addresses_EndDeliveryAddressAddressId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Addresses_StartDeliveryAddressAddressId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Cargos_CargoId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_LogisticCompanies_LogisticCompanyId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_PrivateCompanies_PrivateCompanyId",
                table: "Orders");

            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropIndex(
                name: "IX_Orders_EndDeliveryAddressAddressId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_StartDeliveryAddressAddressId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "EndDeliveryAddressAddressId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "StartDeliveryAddressAddressId",
                table: "Orders");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDeliveryDateTime",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

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

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeliveryDateTime",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CargoId",
                table: "Orders",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "DeliveryEndAddress",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DeliveryStartAddress",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

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
                name: "FK_Orders_PrivateCompanies_PrivateCompanyId",
                table: "Orders",
                column: "PrivateCompanyId",
                principalTable: "PrivateCompanies",
                principalColumn: "PrivateCompanyId");
        }
    }
}

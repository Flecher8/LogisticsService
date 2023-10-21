using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogisticsService.DAL.Migrations
{
    public partial class RemovedPathLengthFieldInOrderTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PathLength",
                table: "Orders");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "PathLength",
                table: "Orders",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}

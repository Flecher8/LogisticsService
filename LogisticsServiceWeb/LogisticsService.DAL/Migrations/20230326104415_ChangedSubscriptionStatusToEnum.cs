using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogisticsService.DAL.Migrations
{
    public partial class ChangedSubscriptionStatusToEnum : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subscriptions_SubscriptionStatuses_SubscriptionStatusId",
                table: "Subscriptions");

            migrationBuilder.DropTable(
                name: "SubscriptionStatuses");

            migrationBuilder.DropIndex(
                name: "IX_Subscriptions_SubscriptionStatusId",
                table: "Subscriptions");

            migrationBuilder.RenameColumn(
                name: "SubscriptionStatusId",
                table: "Subscriptions",
                newName: "SubscriptionStatus");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SubscriptionStatus",
                table: "Subscriptions",
                newName: "SubscriptionStatusId");

            migrationBuilder.CreateTable(
                name: "SubscriptionStatuses",
                columns: table => new
                {
                    SubscriptionStatusId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubscriptionStatusName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriptionStatuses", x => x.SubscriptionStatusId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_SubscriptionStatusId",
                table: "Subscriptions",
                column: "SubscriptionStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Subscriptions_SubscriptionStatuses_SubscriptionStatusId",
                table: "Subscriptions",
                column: "SubscriptionStatusId",
                principalTable: "SubscriptionStatuses",
                principalColumn: "SubscriptionStatusId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

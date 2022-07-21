using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogisticsAssistant.Migrations
{
    public partial class Test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScheduledTrips_Lorries_LorryId",
                table: "ScheduledTrips");

            migrationBuilder.DropIndex(
                name: "IX_ScheduledTrips_LorryId",
                table: "ScheduledTrips");

            migrationBuilder.AlterColumn<string>(
                name: "LorryId",
                table: "ScheduledTrips",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "LorryId1",
                table: "ScheduledTrips",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ScheduledTrips_LorryId1",
                table: "ScheduledTrips",
                column: "LorryId1");

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduledTrips_Lorries_LorryId1",
                table: "ScheduledTrips",
                column: "LorryId1",
                principalTable: "Lorries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScheduledTrips_Lorries_LorryId1",
                table: "ScheduledTrips");

            migrationBuilder.DropIndex(
                name: "IX_ScheduledTrips_LorryId1",
                table: "ScheduledTrips");

            migrationBuilder.DropColumn(
                name: "LorryId1",
                table: "ScheduledTrips");

            migrationBuilder.AlterColumn<int>(
                name: "LorryId",
                table: "ScheduledTrips",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduledTrips_LorryId",
                table: "ScheduledTrips",
                column: "LorryId");

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduledTrips_Lorries_LorryId",
                table: "ScheduledTrips",
                column: "LorryId",
                principalTable: "Lorries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

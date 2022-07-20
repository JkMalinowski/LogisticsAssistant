using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogisticsAssistant.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Lorries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LorryBrand = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaxSpeed = table.Column<int>(type: "int", nullable: false),
                    BreakInMinutes = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lorries", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Lorries");
        }
    }
}

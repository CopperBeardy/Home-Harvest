using Microsoft.EntityFrameworkCore.Migrations;

namespace HomeHarvest.Server.Migrations
{
    public partial class addpoixyforsownaddition : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PoiX",
                table: "Sowns",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PoiY",
                table: "Sowns",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PoiX",
                table: "Sowns");

            migrationBuilder.DropColumn(
                name: "PoiY",
                table: "Sowns");
        }
    }
}

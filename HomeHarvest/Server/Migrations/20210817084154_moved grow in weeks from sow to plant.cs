using Microsoft.EntityFrameworkCore.Migrations;

namespace HomeHarvest.Server.Migrations
{
    public partial class movedgrowinweeksfromsowtoplant : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GrowInWeeks",
                table: "Sows");

            migrationBuilder.AddColumn<double>(
                name: "GrowInWeeks",
                table: "Plants",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GrowInWeeks",
                table: "Plants");

            migrationBuilder.AddColumn<double>(
                name: "GrowInWeeks",
                table: "Sows",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}

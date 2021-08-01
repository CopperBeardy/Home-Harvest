using Microsoft.EntityFrameworkCore.Migrations;

namespace HomeHarvest.Server.Migrations
{
    public partial class ModifiedCroptoincludeadditonalfields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Plot",
                table: "Crops",
                newName: "PlotImage");

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Crops",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Location",
                table: "Crops");

            migrationBuilder.RenameColumn(
                name: "PlotImage",
                table: "Crops",
                newName: "Plot");
        }
    }
}

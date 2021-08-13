using Microsoft.EntityFrameworkCore.Migrations;

namespace HomeHarvest.Server.Migrations
{
	public partial class addedfieldforplotimagename : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AddColumn<string>(
				name: "Plot",
				table: "Crops",
				type: "nvarchar(max)",
				nullable: true);
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropColumn(
				name: "Plot",
				table: "Crops");
		}
	}
}

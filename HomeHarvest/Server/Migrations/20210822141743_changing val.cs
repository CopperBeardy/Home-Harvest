using Microsoft.EntityFrameworkCore.Migrations;

namespace HomeHarvest.Server.Migrations
{
    public partial class changingval : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sowns_Crops_CropId",
                table: "Sowns");

            migrationBuilder.AlterColumn<int>(
                name: "CropId",
                table: "Sowns",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Sowns_Crops_CropId",
                table: "Sowns",
                column: "CropId",
                principalTable: "Crops",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sowns_Crops_CropId",
                table: "Sowns");

            migrationBuilder.AlterColumn<int>(
                name: "CropId",
                table: "Sowns",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Sowns_Crops_CropId",
                table: "Sowns",
                column: "CropId",
                principalTable: "Crops",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

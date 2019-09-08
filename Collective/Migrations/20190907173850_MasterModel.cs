using Microsoft.EntityFrameworkCore.Migrations;

namespace Collective.Migrations
{
    public partial class MasterModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Owned",
                table: "Record",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "TopFive",
                table: "Record",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Owned",
                table: "Record");

            migrationBuilder.DropColumn(
                name: "TopFive",
                table: "Record");
        }
    }
}

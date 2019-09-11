using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Collective.Migrations
{
    public partial class MasterIdChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Record_Community_CommunityId",
                table: "Record");

            migrationBuilder.DropTable(
                name: "Community");

            migrationBuilder.DropIndex(
                name: "IX_Record_CommunityId",
                table: "Record");

            migrationBuilder.DropColumn(
                name: "CommunityId",
                table: "Record");

            migrationBuilder.DropColumn(
                name: "Lowest_Price",
                table: "Record");

            migrationBuilder.DropColumn(
                name: "Owned",
                table: "Record");

            migrationBuilder.DropColumn(
                name: "TopFive",
                table: "Record");

            migrationBuilder.DropColumn(
                name: "TrackList",
                table: "Record");

            migrationBuilder.AddColumn<int>(
                name: "Master_Id",
                table: "Record",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Master_Id",
                table: "Record");

            migrationBuilder.AddColumn<int>(
                name: "CommunityId",
                table: "Record",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Lowest_Price",
                table: "Record",
                nullable: true);

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

            migrationBuilder.AddColumn<string>(
                name: "TrackList",
                table: "Record",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Community",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    have = table.Column<int>(nullable: false),
                    want = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Community", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Record_CommunityId",
                table: "Record",
                column: "CommunityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Record_Community_CommunityId",
                table: "Record",
                column: "CommunityId",
                principalTable: "Community",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

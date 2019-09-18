using Microsoft.EntityFrameworkCore.Migrations;

namespace Collective.Migrations
{
    public partial class UserFriendUpdateId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserFriend_AspNetUsers_ApplicationUserId1",
                table: "UserFriend");

            migrationBuilder.DropForeignKey(
                name: "FK_UserFriend_AspNetUsers_FriendId1",
                table: "UserFriend");

            migrationBuilder.DropIndex(
                name: "IX_UserFriend_ApplicationUserId1",
                table: "UserFriend");

            migrationBuilder.DropIndex(
                name: "IX_UserFriend_FriendId1",
                table: "UserFriend");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId1",
                table: "UserFriend");

            migrationBuilder.DropColumn(
                name: "FriendId1",
                table: "UserFriend");

            migrationBuilder.AlterColumn<string>(
                name: "FriendId",
                table: "UserFriend",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "UserFriend",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_UserFriend_ApplicationUserId",
                table: "UserFriend",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserFriend_FriendId",
                table: "UserFriend",
                column: "FriendId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserFriend_AspNetUsers_ApplicationUserId",
                table: "UserFriend",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserFriend_AspNetUsers_FriendId",
                table: "UserFriend",
                column: "FriendId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserFriend_AspNetUsers_ApplicationUserId",
                table: "UserFriend");

            migrationBuilder.DropForeignKey(
                name: "FK_UserFriend_AspNetUsers_FriendId",
                table: "UserFriend");

            migrationBuilder.DropIndex(
                name: "IX_UserFriend_ApplicationUserId",
                table: "UserFriend");

            migrationBuilder.DropIndex(
                name: "IX_UserFriend_FriendId",
                table: "UserFriend");

            migrationBuilder.AlterColumn<int>(
                name: "FriendId",
                table: "UserFriend",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ApplicationUserId",
                table: "UserFriend",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId1",
                table: "UserFriend",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FriendId1",
                table: "UserFriend",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserFriend_ApplicationUserId1",
                table: "UserFriend",
                column: "ApplicationUserId1");

            migrationBuilder.CreateIndex(
                name: "IX_UserFriend_FriendId1",
                table: "UserFriend",
                column: "FriendId1");

            migrationBuilder.AddForeignKey(
                name: "FK_UserFriend_AspNetUsers_ApplicationUserId1",
                table: "UserFriend",
                column: "ApplicationUserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserFriend_AspNetUsers_FriendId1",
                table: "UserFriend",
                column: "FriendId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

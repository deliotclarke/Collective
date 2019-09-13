using Microsoft.EntityFrameworkCore.Migrations;

namespace Collective.Migrations
{
    public partial class FixedMemoryUserId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Memory_AspNetUsers_ApplicationUserId1",
                table: "Memory");

            migrationBuilder.DropForeignKey(
                name: "FK_UserFriend_AspNetUsers_ApplicationUserFriendId",
                table: "UserFriend");

            migrationBuilder.DropIndex(
                name: "IX_Memory_ApplicationUserId1",
                table: "Memory");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId1",
                table: "Memory");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserFriendId",
                table: "UserFriend",
                newName: "FriendId1");

            migrationBuilder.RenameIndex(
                name: "IX_UserFriend_ApplicationUserFriendId",
                table: "UserFriend",
                newName: "IX_UserFriend_FriendId1");

            migrationBuilder.RenameColumn(
                name: "UserImg",
                table: "AspNetUsers",
                newName: "UserImgPath");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "Memory",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_Memory_ApplicationUserId",
                table: "Memory",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Memory_AspNetUsers_ApplicationUserId",
                table: "Memory",
                column: "ApplicationUserId",
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Memory_AspNetUsers_ApplicationUserId",
                table: "Memory");

            migrationBuilder.DropForeignKey(
                name: "FK_UserFriend_AspNetUsers_FriendId1",
                table: "UserFriend");

            migrationBuilder.DropIndex(
                name: "IX_Memory_ApplicationUserId",
                table: "Memory");

            migrationBuilder.RenameColumn(
                name: "FriendId1",
                table: "UserFriend",
                newName: "ApplicationUserFriendId");

            migrationBuilder.RenameIndex(
                name: "IX_UserFriend_FriendId1",
                table: "UserFriend",
                newName: "IX_UserFriend_ApplicationUserFriendId");

            migrationBuilder.RenameColumn(
                name: "UserImgPath",
                table: "AspNetUsers",
                newName: "UserImg");

            migrationBuilder.AlterColumn<int>(
                name: "ApplicationUserId",
                table: "Memory",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId1",
                table: "Memory",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Memory_ApplicationUserId1",
                table: "Memory",
                column: "ApplicationUserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Memory_AspNetUsers_ApplicationUserId1",
                table: "Memory",
                column: "ApplicationUserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserFriend_AspNetUsers_ApplicationUserFriendId",
                table: "UserFriend",
                column: "ApplicationUserFriendId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

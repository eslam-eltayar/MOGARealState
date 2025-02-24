using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MOGARealState.Repositories._Data.Migrations
{
    /// <inheritdoc />
    public partial class AddFavAPPUserProps : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteUserProperties_AspNetUsers_UserId",
                table: "FavoriteUserProperties");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "FavoriteUserProperties",
                newName: "AppUserId");

            migrationBuilder.RenameIndex(
                name: "IX_FavoriteUserProperties_UserId",
                table: "FavoriteUserProperties",
                newName: "IX_FavoriteUserProperties_AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteUserProperties_AspNetUsers_AppUserId",
                table: "FavoriteUserProperties",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteUserProperties_AspNetUsers_AppUserId",
                table: "FavoriteUserProperties");

            migrationBuilder.RenameColumn(
                name: "AppUserId",
                table: "FavoriteUserProperties",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_FavoriteUserProperties_AppUserId",
                table: "FavoriteUserProperties",
                newName: "IX_FavoriteUserProperties_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteUserProperties_AspNetUsers_UserId",
                table: "FavoriteUserProperties",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

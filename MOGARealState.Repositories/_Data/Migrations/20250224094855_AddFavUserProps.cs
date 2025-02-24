using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MOGARealState.Repositories._Data.Migrations
{
    /// <inheritdoc />
    public partial class AddFavUserProps : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FavoriteUserProperties",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PropertyId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavoriteUserProperties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FavoriteUserProperties_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FavoriteUserProperties_Properties_PropertyId",
                        column: x => x.PropertyId,
                        principalTable: "Properties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteUserProperties_PropertyId",
                table: "FavoriteUserProperties",
                column: "PropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteUserProperties_UserId",
                table: "FavoriteUserProperties",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FavoriteUserProperties");
        }
    }
}

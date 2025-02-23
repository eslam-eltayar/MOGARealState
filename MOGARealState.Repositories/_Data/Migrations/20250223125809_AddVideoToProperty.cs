using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MOGARealState.Repositories._Data.Migrations
{
    /// <inheritdoc />
    public partial class AddVideoToProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FloorsCount",
                table: "Properties",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsFurnished",
                table: "Properties",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "VideoUrl",
                table: "Properties",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FloorsCount",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "IsFurnished",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "VideoUrl",
                table: "Properties");
        }
    }
}

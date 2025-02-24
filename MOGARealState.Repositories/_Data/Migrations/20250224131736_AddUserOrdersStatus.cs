using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MOGARealState.Repositories._Data.Migrations
{
    /// <inheritdoc />
    public partial class AddUserOrdersStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "UserOrders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "UserOrders");
        }
    }
}

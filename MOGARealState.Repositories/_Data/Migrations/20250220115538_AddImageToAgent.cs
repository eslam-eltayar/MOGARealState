﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MOGARealState.Repositories._Data.Migrations
{
    /// <inheritdoc />
    public partial class AddImageToAgent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Agents",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Agents");
        }
    }
}

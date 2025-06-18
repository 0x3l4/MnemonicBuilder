using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MnemonicBuilder.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPublicToSet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPublic",
                table: "sets",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPublic",
                table: "sets");
        }
    }
}

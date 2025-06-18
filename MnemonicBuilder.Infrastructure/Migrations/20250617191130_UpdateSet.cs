using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MnemonicBuilder.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Text",
                table: "sets");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Text",
                table: "sets",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}

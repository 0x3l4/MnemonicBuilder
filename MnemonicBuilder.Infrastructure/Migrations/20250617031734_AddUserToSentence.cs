using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MnemonicBuilder.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddUserToSentence : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "sentences",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_sentences_UserId",
                table: "sentences",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_sentences_AspNetUsers_UserId",
                table: "sentences",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_sentences_AspNetUsers_UserId",
                table: "sentences");

            migrationBuilder.DropIndex(
                name: "IX_sentences_UserId",
                table: "sentences");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "sentences");
        }
    }
}

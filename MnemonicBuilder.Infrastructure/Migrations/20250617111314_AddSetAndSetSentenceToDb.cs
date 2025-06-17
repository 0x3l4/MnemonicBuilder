using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MnemonicBuilder.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddSetAndSetSentenceToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "sets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Text = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_sets_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SetSentences",
                columns: table => new
                {
                    SetId = table.Column<int>(type: "integer", nullable: false),
                    SentenceId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SetSentences", x => new { x.SetId, x.SentenceId });
                    table.ForeignKey(
                        name: "FK_SetSentences_sentences_SentenceId",
                        column: x => x.SentenceId,
                        principalTable: "sentences",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SetSentences_sets_SetId",
                        column: x => x.SetId,
                        principalTable: "sets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_sets_UserId",
                table: "sets",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SetSentences_SentenceId",
                table: "SetSentences",
                column: "SentenceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SetSentences");

            migrationBuilder.DropTable(
                name: "sets");
        }
    }
}

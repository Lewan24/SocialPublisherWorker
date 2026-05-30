using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SocialPublisherWorker.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PostPublications",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Platform = table.Column<int>(type: "INTEGER", nullable: false),
                    WeekStart = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    PublishedAtUtc = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    ExternalPostId = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostPublications", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PostPublications_Platform_WeekStart",
                table: "PostPublications",
                columns: new[] { "Platform", "WeekStart" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PostPublications");
        }
    }
}

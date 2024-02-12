using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class removed_unwanted_tables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BoxOffice");

            migrationBuilder.DropTable(
                name: "OTTRatings");

            migrationBuilder.DropTable(
                name: "Teaser");

            migrationBuilder.DropTable(
                name: "Trailer");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "31135b3acbfd476ead4106a936a646c2");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "973d644ee3734ca88a34c5e813718004");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "f080fd72f67945cd8ed12d9cba785be9");

            migrationBuilder.AlterColumn<string>(
                name: "FilmName",
                table: "AdminReview",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "FilmName",
                table: "AdminReview",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "BoxOffice",
                columns: table => new
                {
                    BoxOfficeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FilmName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    LanguageName = table.Column<int>(type: "int", nullable: true),
                    Rating = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoxOffice", x => x.BoxOfficeID);
                });

            migrationBuilder.CreateTable(
                name: "OTTRatings",
                columns: table => new
                {
                    OTTID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FilmName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Likes = table.Column<int>(type: "int", nullable: false),
                    OTTPlatform = table.Column<int>(type: "int", nullable: false),
                    ReleasedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Views = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OTTRatings", x => x.OTTID);
                });

            migrationBuilder.CreateTable(
                name: "Teaser",
                columns: table => new
                {
                    TeaserID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FilmName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Likes = table.Column<int>(type: "int", nullable: false),
                    ReleasedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Views = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teaser", x => x.TeaserID);
                });

            migrationBuilder.CreateTable(
                name: "Trailer",
                columns: table => new
                {
                    TrailerID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FilmName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Likes = table.Column<int>(type: "int", nullable: false),
                    ReleasedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Views = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trailer", x => x.TrailerID);
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "31135b3acbfd476ead4106a936a646c2", null, "OTTadmin", null },
                    { "973d644ee3734ca88a34c5e813718004", null, "Producer", null },
                    { "f080fd72f67945cd8ed12d9cba785be9", null, "Admin", null }
                });
        }
    }
}

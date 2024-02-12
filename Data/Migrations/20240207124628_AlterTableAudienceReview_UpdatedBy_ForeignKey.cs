using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class AlterTableAudienceReview_UpdatedBy_ForeignKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "AudienceReview",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AudienceReview_UserId",
                table: "AudienceReview",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AudienceReview_Users_UserId",
                table: "AudienceReview",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AudienceReview_Users_UserId",
                table: "AudienceReview");

            migrationBuilder.DropIndex(
                name: "IX_AudienceReview_UserId",
                table: "AudienceReview");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "AudienceReview");
        }
    }
}

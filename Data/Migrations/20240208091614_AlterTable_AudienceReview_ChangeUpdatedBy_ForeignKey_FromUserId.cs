using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class AlterTable_AudienceReview_ChangeUpdatedBy_ForeignKey_FromUserId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedBy",
                table: "AudienceReview",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_AudienceReview_UpdatedBy",
                table: "AudienceReview",
                column: "UpdatedBy");

            migrationBuilder.AddForeignKey(
                name: "FK_AudienceReview_Users_UpdatedBy",
                table: "AudienceReview",
                column: "UpdatedBy",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AudienceReview_Users_UpdatedBy",
                table: "AudienceReview");

            migrationBuilder.DropIndex(
                name: "IX_AudienceReview_UpdatedBy",
                table: "AudienceReview");

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedBy",
                table: "AudienceReview",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

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
    }
}

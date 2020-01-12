using Microsoft.EntityFrameworkCore.Migrations;

namespace KeyForgeGameTracker.Migrations
{
    public partial class UniqueConstraints : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "KeyForgeId",
                table: "House",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AppUserId",
                table: "Deck",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_House_KeyForgeId",
                table: "House",
                column: "KeyForgeId",
                unique: true,
                filter: "[KeyForgeId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Deck_AppUserId",
                table: "Deck",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Deck_KeyForgeId",
                table: "Deck",
                column: "KeyForgeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Card_KeyForgeId",
                table: "Card",
                column: "KeyForgeId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Deck_AspNetUsers_AppUserId",
                table: "Deck",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Deck_AspNetUsers_AppUserId",
                table: "Deck");

            migrationBuilder.DropIndex(
                name: "IX_House_KeyForgeId",
                table: "House");

            migrationBuilder.DropIndex(
                name: "IX_Deck_AppUserId",
                table: "Deck");

            migrationBuilder.DropIndex(
                name: "IX_Deck_KeyForgeId",
                table: "Deck");

            migrationBuilder.DropIndex(
                name: "IX_Card_KeyForgeId",
                table: "Card");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Deck");

            migrationBuilder.AlterColumn<string>(
                name: "KeyForgeId",
                table: "House",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}

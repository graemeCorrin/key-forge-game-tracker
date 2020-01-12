using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace KeyForgeGameTracker.Migrations
{
    public partial class AllModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Deck",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<string>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    KeyForgeId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Expansion = table.Column<int>(nullable: false),
                    PowerLevel = table.Column<int>(nullable: false),
                    Chains = table.Column<int>(nullable: false),
                    Wins = table.Column<int>(nullable: false),
                    Loses = table.Column<int>(nullable: false),
                    Notes = table.Column<string>(nullable: true),
                    Alias = table.Column<string>(nullable: true),
                    MyNotes = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deck", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "House",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<string>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    KeyForgeId = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Image = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_House", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Game",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<string>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    GameDate = table.Column<DateTime>(nullable: false),
                    Comments = table.Column<string>(nullable: true),
                    Swap = table.Column<bool>(nullable: false),
                    WinningPlayerId = table.Column<int>(nullable: true),
                    LosingPlayerId = table.Column<int>(nullable: true),
                    WinningDeckId = table.Column<int>(nullable: true),
                    LosingDeckId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Game", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Game_Deck_LosingDeckId",
                        column: x => x.LosingDeckId,
                        principalTable: "Deck",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Game_AspNetUsers_LosingPlayerId",
                        column: x => x.LosingPlayerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Game_Deck_WinningDeckId",
                        column: x => x.WinningDeckId,
                        principalTable: "Deck",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Game_AspNetUsers_WinningPlayerId",
                        column: x => x.WinningPlayerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Card",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<string>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    KeyForgeId = table.Column<Guid>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    HouseId = table.Column<int>(nullable: true),
                    CardType = table.Column<string>(nullable: true),
                    FrontImage = table.Column<string>(nullable: true),
                    CardText = table.Column<string>(nullable: true),
                    Traits = table.Column<string>(nullable: true),
                    Amber = table.Column<int>(nullable: false),
                    Power = table.Column<string>(nullable: true),
                    Armor = table.Column<string>(nullable: true),
                    Rarity = table.Column<string>(nullable: true),
                    FlavorText = table.Column<string>(nullable: true),
                    CardNumber = table.Column<string>(nullable: true),
                    Expansion = table.Column<int>(nullable: false),
                    IsMaverick = table.Column<bool>(nullable: false),
                    IsAnomaly = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Card", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Card_House_HouseId",
                        column: x => x.HouseId,
                        principalTable: "House",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DeckHouse",
                columns: table => new
                {
                    DeckId = table.Column<int>(nullable: false),
                    HouseId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeckHouse", x => new { x.DeckId, x.HouseId });
                    table.ForeignKey(
                        name: "FK_DeckHouse_Deck_DeckId",
                        column: x => x.DeckId,
                        principalTable: "Deck",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DeckHouse_House_HouseId",
                        column: x => x.HouseId,
                        principalTable: "House",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DeckCard",
                columns: table => new
                {
                    DeckId = table.Column<int>(nullable: false),
                    CardId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeckCard", x => new { x.DeckId, x.CardId });
                    table.ForeignKey(
                        name: "FK_DeckCard_Card_CardId",
                        column: x => x.CardId,
                        principalTable: "Card",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DeckCard_Deck_DeckId",
                        column: x => x.DeckId,
                        principalTable: "Deck",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Card_HouseId",
                table: "Card",
                column: "HouseId");

            migrationBuilder.CreateIndex(
                name: "IX_DeckCard_CardId",
                table: "DeckCard",
                column: "CardId");

            migrationBuilder.CreateIndex(
                name: "IX_DeckHouse_HouseId",
                table: "DeckHouse",
                column: "HouseId");

            migrationBuilder.CreateIndex(
                name: "IX_Game_LosingDeckId",
                table: "Game",
                column: "LosingDeckId");

            migrationBuilder.CreateIndex(
                name: "IX_Game_LosingPlayerId",
                table: "Game",
                column: "LosingPlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Game_WinningDeckId",
                table: "Game",
                column: "WinningDeckId");

            migrationBuilder.CreateIndex(
                name: "IX_Game_WinningPlayerId",
                table: "Game",
                column: "WinningPlayerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeckCard");

            migrationBuilder.DropTable(
                name: "DeckHouse");

            migrationBuilder.DropTable(
                name: "Game");

            migrationBuilder.DropTable(
                name: "Card");

            migrationBuilder.DropTable(
                name: "Deck");

            migrationBuilder.DropTable(
                name: "House");
        }
    }
}

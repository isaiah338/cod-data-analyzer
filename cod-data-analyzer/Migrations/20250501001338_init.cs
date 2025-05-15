using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cod_data_analyzer.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Map",
                columns: table => new
                {
                    MapId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MapName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsFaceoff = table.Column<bool>(type: "bit", nullable: false),
                    IsSmall = table.Column<bool>(type: "bit", nullable: false),
                    IsSpecialty = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Map", x => x.MapId);
                });

            migrationBuilder.CreateTable(
                name: "Match",
                columns: table => new
                {
                    MatchId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MatchStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MatchEnd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MatchWin = table.Column<bool>(type: "bit", nullable: false),
                    MapId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Match", x => x.MatchId);
                    table.ForeignKey(
                        name: "FK_Match_Map_MapId",
                        column: x => x.MapId,
                        principalTable: "Map",
                        principalColumn: "MapId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LifetimeStats",
                columns: table => new
                {
                    LifetimeStatsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LifetimeWins = table.Column<int>(type: "int", nullable: false),
                    LifetimeLosses = table.Column<int>(type: "int", nullable: false),
                    LifetimeKills = table.Column<int>(type: "int", nullable: false),
                    LifetimeDeaths = table.Column<int>(type: "int", nullable: false),
                    LifetimeHits = table.Column<int>(type: "int", nullable: false),
                    LifetimeMisses = table.Column<int>(type: "int", nullable: false),
                    MatchId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LifetimeStats", x => x.LifetimeStatsId);
                    table.ForeignKey(
                        name: "FK_LifetimeStats_Match_MatchId",
                        column: x => x.MatchId,
                        principalTable: "Match",
                        principalColumn: "MatchId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlayerStats",
                columns: table => new
                {
                    PlayerStatsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Skill = table.Column<int>(type: "int", nullable: false),
                    Score = table.Column<int>(type: "int", nullable: false),
                    Shots = table.Column<int>(type: "int", nullable: false),
                    LongestStreak = table.Column<int>(type: "int", nullable: false),
                    Headshots = table.Column<int>(type: "int", nullable: false),
                    Executions = table.Column<int>(type: "int", nullable: false),
                    Suicides = table.Column<int>(type: "int", nullable: false),
                    PercentTimeMoving = table.Column<double>(type: "float", nullable: false),
                    Kills = table.Column<int>(type: "int", nullable: false),
                    Deaths = table.Column<int>(type: "int", nullable: false),
                    DamageDone = table.Column<int>(type: "int", nullable: false),
                    Hits = table.Column<int>(type: "int", nullable: false),
                    Assists = table.Column<int>(type: "int", nullable: false),
                    MatchId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerStats", x => x.PlayerStatsId);
                    table.ForeignKey(
                        name: "FK_PlayerStats_Match_MatchId",
                        column: x => x.MatchId,
                        principalTable: "Match",
                        principalColumn: "MatchId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "XpStats",
                columns: table => new
                {
                    XpStatsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TotalXp = table.Column<int>(type: "int", nullable: false),
                    ChallengeXp = table.Column<int>(type: "int", nullable: false),
                    ScoreXp = table.Column<int>(type: "int", nullable: false),
                    MatchXp = table.Column<int>(type: "int", nullable: false),
                    MedalXp = table.Column<int>(type: "int", nullable: false),
                    MiscXp = table.Column<int>(type: "int", nullable: false),
                    AccoladeXp = table.Column<int>(type: "int", nullable: false),
                    WeaponXp = table.Column<int>(type: "int", nullable: false),
                    OperatorXp = table.Column<int>(type: "int", nullable: false),
                    BattlepassXp = table.Column<int>(type: "int", nullable: false),
                    RankStart = table.Column<int>(type: "int", nullable: false),
                    RankEnd = table.Column<int>(type: "int", nullable: false),
                    MatchId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_XpStats", x => x.XpStatsId);
                    table.ForeignKey(
                        name: "FK_XpStats_Match_MatchId",
                        column: x => x.MatchId,
                        principalTable: "Match",
                        principalColumn: "MatchId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LifetimeStats_MatchId",
                table: "LifetimeStats",
                column: "MatchId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Match_MapId",
                table: "Match",
                column: "MapId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerStats_MatchId",
                table: "PlayerStats",
                column: "MatchId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_XpStats_MatchId",
                table: "XpStats",
                column: "MatchId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LifetimeStats");

            migrationBuilder.DropTable(
                name: "PlayerStats");

            migrationBuilder.DropTable(
                name: "XpStats");

            migrationBuilder.DropTable(
                name: "Match");

            migrationBuilder.DropTable(
                name: "Map");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cod_data_analyzer.Migrations
{
    /// <inheritdoc />
    public partial class analyzersettings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AnalyzerSettingsId",
                table: "Map",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AnalyzerSettingsId",
                table: "GameMode",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AnalyzerSettings",
                columns: table => new
                {
                    AnalyzerSettingsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MatchQuitThreshold = table.Column<int>(type: "int", nullable: false),
                    MinTimePlayedTopGame = table.Column<int>(type: "int", nullable: false),
                    MinTimePlayedTopMap = table.Column<int>(type: "int", nullable: false),
                    MinScore = table.Column<int>(type: "int", nullable: false),
                    MinSkill = table.Column<int>(type: "int", nullable: false),
                    isMapWhitelist = table.Column<bool>(type: "bit", nullable: false),
                    isGameModeWhitelist = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnalyzerSettings", x => x.AnalyzerSettingsId);
                });

            migrationBuilder.CreateTable(
                name: "AnalyzerSettingsMap",
                columns: table => new
                {
                    AnalyzerSettingsMapId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AnalyzerSettingsId = table.Column<int>(type: "int", nullable: false),
                    MapId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnalyzerSettingsMap", x => x.AnalyzerSettingsMapId);
                    table.ForeignKey(
                        name: "FK_AnalyzerSettingsMap_AnalyzerSettings_AnalyzerSettingsId",
                        column: x => x.AnalyzerSettingsId,
                        principalTable: "AnalyzerSettings",
                        principalColumn: "AnalyzerSettingsId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnalyzerSettingsMap_Map_MapId",
                        column: x => x.MapId,
                        principalTable: "Map",
                        principalColumn: "MapId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AnalyzerSettingsMode",
                columns: table => new
                {
                    AnalyzerSettingsModeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AnalyzerSettingsId = table.Column<int>(type: "int", nullable: false),
                    GameModeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnalyzerSettingsMode", x => x.AnalyzerSettingsModeId);
                    table.ForeignKey(
                        name: "FK_AnalyzerSettingsMode_AnalyzerSettings_AnalyzerSettingsId",
                        column: x => x.AnalyzerSettingsId,
                        principalTable: "AnalyzerSettings",
                        principalColumn: "AnalyzerSettingsId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnalyzerSettingsMode_GameMode_GameModeId",
                        column: x => x.GameModeId,
                        principalTable: "GameMode",
                        principalColumn: "GameModeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Map_AnalyzerSettingsId",
                table: "Map",
                column: "AnalyzerSettingsId");

            migrationBuilder.CreateIndex(
                name: "IX_GameMode_AnalyzerSettingsId",
                table: "GameMode",
                column: "AnalyzerSettingsId");

            migrationBuilder.CreateIndex(
                name: "IX_AnalyzerSettingsMap_AnalyzerSettingsId",
                table: "AnalyzerSettingsMap",
                column: "AnalyzerSettingsId");

            migrationBuilder.CreateIndex(
                name: "IX_AnalyzerSettingsMap_MapId",
                table: "AnalyzerSettingsMap",
                column: "MapId");

            migrationBuilder.CreateIndex(
                name: "IX_AnalyzerSettingsMode_AnalyzerSettingsId",
                table: "AnalyzerSettingsMode",
                column: "AnalyzerSettingsId");

            migrationBuilder.CreateIndex(
                name: "IX_AnalyzerSettingsMode_GameModeId",
                table: "AnalyzerSettingsMode",
                column: "GameModeId");

            migrationBuilder.AddForeignKey(
                name: "FK_GameMode_AnalyzerSettings_AnalyzerSettingsId",
                table: "GameMode",
                column: "AnalyzerSettingsId",
                principalTable: "AnalyzerSettings",
                principalColumn: "AnalyzerSettingsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Map_AnalyzerSettings_AnalyzerSettingsId",
                table: "Map",
                column: "AnalyzerSettingsId",
                principalTable: "AnalyzerSettings",
                principalColumn: "AnalyzerSettingsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameMode_AnalyzerSettings_AnalyzerSettingsId",
                table: "GameMode");

            migrationBuilder.DropForeignKey(
                name: "FK_Map_AnalyzerSettings_AnalyzerSettingsId",
                table: "Map");

            migrationBuilder.DropTable(
                name: "AnalyzerSettingsMap");

            migrationBuilder.DropTable(
                name: "AnalyzerSettingsMode");

            migrationBuilder.DropTable(
                name: "AnalyzerSettings");

            migrationBuilder.DropIndex(
                name: "IX_Map_AnalyzerSettingsId",
                table: "Map");

            migrationBuilder.DropIndex(
                name: "IX_GameMode_AnalyzerSettingsId",
                table: "GameMode");

            migrationBuilder.DropColumn(
                name: "AnalyzerSettingsId",
                table: "Map");

            migrationBuilder.DropColumn(
                name: "AnalyzerSettingsId",
                table: "GameMode");
        }
    }
}

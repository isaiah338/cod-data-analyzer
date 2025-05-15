using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cod_data_analyzer.Migrations
{
    /// <inheritdoc />
    public partial class analyzersettings2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnalyzerSettingsMode");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                name: "IX_AnalyzerSettingsMode_AnalyzerSettingsId",
                table: "AnalyzerSettingsMode",
                column: "AnalyzerSettingsId");

            migrationBuilder.CreateIndex(
                name: "IX_AnalyzerSettingsMode_GameModeId",
                table: "AnalyzerSettingsMode",
                column: "GameModeId");
        }
    }
}

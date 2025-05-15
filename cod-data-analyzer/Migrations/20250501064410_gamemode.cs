using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cod_data_analyzer.Migrations
{
    /// <inheritdoc />
    public partial class gamemode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GameTitle",
                columns: table => new
                {
                    GameModeTitleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GameModeTitleCode = table.Column<int>(type: "int", nullable: false),
                    GameModeTitleName = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameTitle", x => x.GameModeTitleId);
                });

            migrationBuilder.CreateTable(
                name: "GameType",
                columns: table => new
                {
                    GameModeTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GameModeTypeName = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameType", x => x.GameModeTypeId);
                });

            migrationBuilder.CreateTable(
                name: "GameMode",
                columns: table => new
                {
                    GameModeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GameModeDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GameTypeId = table.Column<int>(type: "int", nullable: false),
                    GameTitleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameMode", x => x.GameModeId);
                    table.ForeignKey(
                        name: "FK_GameMode_GameTitle_GameTitleId",
                        column: x => x.GameTitleId,
                        principalTable: "GameTitle",
                        principalColumn: "GameModeTitleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameMode_GameType_GameTypeId",
                        column: x => x.GameTypeId,
                        principalTable: "GameType",
                        principalColumn: "GameModeTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GameMode_GameTitleId",
                table: "GameMode",
                column: "GameTitleId");

            migrationBuilder.CreateIndex(
                name: "IX_GameMode_GameTypeId",
                table: "GameMode",
                column: "GameTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameMode");

            migrationBuilder.DropTable(
                name: "GameTitle");

            migrationBuilder.DropTable(
                name: "GameType");
        }
    }
}

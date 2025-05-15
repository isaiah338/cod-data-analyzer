using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cod_data_analyzer.Migrations
{
    /// <inheritdoc />
    public partial class correctedgamemode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "GameModeTypeName",
                table: "GameType",
                newName: "GameTypeName");

            migrationBuilder.RenameColumn(
                name: "GameModeTypeId",
                table: "GameType",
                newName: "GameTypeId");

            migrationBuilder.RenameColumn(
                name: "GameModeTitleName",
                table: "GameTitle",
                newName: "GameTitleName");

            migrationBuilder.RenameColumn(
                name: "GameModeTitleCode",
                table: "GameTitle",
                newName: "GameTitleCode");

            migrationBuilder.RenameColumn(
                name: "GameModeTitleId",
                table: "GameTitle",
                newName: "GameTitleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "GameTypeName",
                table: "GameType",
                newName: "GameModeTypeName");

            migrationBuilder.RenameColumn(
                name: "GameTypeId",
                table: "GameType",
                newName: "GameModeTypeId");

            migrationBuilder.RenameColumn(
                name: "GameTitleName",
                table: "GameTitle",
                newName: "GameModeTitleName");

            migrationBuilder.RenameColumn(
                name: "GameTitleCode",
                table: "GameTitle",
                newName: "GameModeTitleCode");

            migrationBuilder.RenameColumn(
                name: "GameTitleId",
                table: "GameTitle",
                newName: "GameModeTitleId");
        }
    }
}

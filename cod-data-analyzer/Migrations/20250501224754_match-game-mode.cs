using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cod_data_analyzer.Migrations
{
    /// <inheritdoc />
    public partial class matchgamemode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GameModeId",
                table: "Match",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Match_GameModeId",
                table: "Match",
                column: "GameModeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Match_GameMode_GameModeId",
                table: "Match",
                column: "GameModeId",
                principalTable: "GameMode",
                principalColumn: "GameModeId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Match_GameMode_GameModeId",
                table: "Match");

            migrationBuilder.DropIndex(
                name: "IX_Match_GameModeId",
                table: "Match");

            migrationBuilder.DropColumn(
                name: "GameModeId",
                table: "Match");
        }
    }
}

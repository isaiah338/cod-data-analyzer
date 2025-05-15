using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cod_data_analyzer.Migrations
{
    /// <inheritdoc />
    public partial class usermatches : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Match",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Match_UserId",
                table: "Match",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Match_AspNetUsers_UserId",
                table: "Match",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Match_AspNetUsers_UserId",
                table: "Match");

            migrationBuilder.DropIndex(
                name: "IX_Match_UserId",
                table: "Match");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Match",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);
        }
    }
}

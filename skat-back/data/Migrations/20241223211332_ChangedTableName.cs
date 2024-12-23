using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace skat_back.Migrations
{
    /// <inheritdoc />
    public partial class ChangedTableName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TotalMatchDays_Users_UserId",
                table: "TotalMatchDays");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TotalMatchDays",
                table: "TotalMatchDays");

            migrationBuilder.RenameTable(
                name: "TotalMatchDays",
                newName: "AggregatedMatchDayStats");

            migrationBuilder.RenameIndex(
                name: "IX_TotalMatchDays_UserId",
                table: "AggregatedMatchDayStats",
                newName: "IX_AggregatedMatchDayStats_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AggregatedMatchDayStats",
                table: "AggregatedMatchDayStats",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AggregatedMatchDayStats_Users_UserId",
                table: "AggregatedMatchDayStats",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AggregatedMatchDayStats_Users_UserId",
                table: "AggregatedMatchDayStats");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AggregatedMatchDayStats",
                table: "AggregatedMatchDayStats");

            migrationBuilder.RenameTable(
                name: "AggregatedMatchDayStats",
                newName: "TotalMatchDays");

            migrationBuilder.RenameIndex(
                name: "IX_AggregatedMatchDayStats_UserId",
                table: "TotalMatchDays",
                newName: "IX_TotalMatchDays_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TotalMatchDays",
                table: "TotalMatchDays",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TotalMatchDays_Users_UserId",
                table: "TotalMatchDays",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

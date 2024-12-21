using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace skat_back.Migrations
{
    /// <inheritdoc />
    public partial class AddedFieldsToMatchDay : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PointsChangeFromLastMatch",
                table: "MatchDays",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TotalMatchShare",
                table: "MatchDays",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TotalPoints",
                table: "MatchDays",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PointsChangeFromLastMatch",
                table: "MatchDays");

            migrationBuilder.DropColumn(
                name: "TotalMatchShare",
                table: "MatchDays");

            migrationBuilder.DropColumn(
                name: "TotalPoints",
                table: "MatchDays");
        }
    }
}

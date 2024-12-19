using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace skat_back.Migrations
{
    /// <inheritdoc />
    public partial class ChangedTableStructure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_Matchdays_MatchdayId",
                table: "Games");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Games",
                table: "Games");

            migrationBuilder.RenameTable(
                name: "Games",
                newName: "Matches");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Matches",
                newName: "UpdatedAt");

            migrationBuilder.RenameIndex(
                name: "IX_Games_MatchdayId",
                table: "Matches",
                newName: "IX_Matches_MatchdayId");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Matches",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "PlayerId",
                table: "Matches",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Points",
                table: "Matches",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Table",
                table: "Matches",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Matches",
                table: "Matches",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Matches_PlayerId",
                table: "Matches",
                column: "PlayerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Matchdays_MatchdayId",
                table: "Matches",
                column: "MatchdayId",
                principalTable: "Matchdays",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Players_PlayerId",
                table: "Matches",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Matchdays_MatchdayId",
                table: "Matches");

            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Players_PlayerId",
                table: "Matches");

            migrationBuilder.DropTable(
                name: "Players");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Matches",
                table: "Matches");

            migrationBuilder.DropIndex(
                name: "IX_Matches_PlayerId",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "PlayerId",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "Points",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "Table",
                table: "Matches");

            migrationBuilder.RenameTable(
                name: "Matches",
                newName: "Games");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "Games",
                newName: "Name");

            migrationBuilder.RenameIndex(
                name: "IX_Matches_MatchdayId",
                table: "Games",
                newName: "IX_Games_MatchdayId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Games",
                table: "Games",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Matchdays_MatchdayId",
                table: "Games",
                column: "MatchdayId",
                principalTable: "Matchdays",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

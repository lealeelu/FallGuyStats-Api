// FallGuyStats Copyright (c) Leah Lee. All rights reserved.
// Licensed under the GNU General Public License v3.0
// Please Consider supporting the developer with some good good coffee: ko-fi.com/lealeelu

using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FallGuyStats.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Episodes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Kudos = table.Column<int>(nullable: false),
                    Fame = table.Column<int>(nullable: false),
                    Crowns = table.Column<int>(nullable: false),
                    Season = table.Column<int>(nullable: false),
                    Timestamp = table.Column<string>(nullable: true),
                    EpisodeFinished = table.Column<DateTime>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Episodes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rounds",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    EpisodeId = table.Column<int>(nullable: false),
                    RoundType = table.Column<string>(nullable: true),
                    Qualified = table.Column<bool>(nullable: false),
                    Position = table.Column<int>(nullable: false),
                    Kudos = table.Column<int>(nullable: false),
                    Fame = table.Column<int>(nullable: false),
                    BonusTier = table.Column<int>(nullable: false),
                    BonusKudos = table.Column<int>(nullable: false),
                    BonusFame = table.Column<int>(nullable: false),
                    Badge = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rounds", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Episodes");

            migrationBuilder.DropTable(
                name: "Rounds");
        }
    }
}

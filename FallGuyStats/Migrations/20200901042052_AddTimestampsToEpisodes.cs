using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FallGuyStats.Migrations
{
    public partial class AddTimestampsToEpisodes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "Episodes",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Timestamp",
                table: "Episodes",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Created",
                table: "Episodes");

            migrationBuilder.DropColumn(
                name: "Timestamp",
                table: "Episodes");
        }
    }
}

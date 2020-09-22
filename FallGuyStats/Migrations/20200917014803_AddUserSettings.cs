using Microsoft.EntityFrameworkCore.Migrations;

namespace FallGuyStats.Migrations
{
    public partial class AddUserSettings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserSettings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PollingFrequency = table.Column<int>(nullable: false),
                    ShowCheaterCount = table.Column<bool>(nullable: false),                    
                    ShowLastEpisode = table.Column<bool>(nullable: false),
                    ShowLosingStreak = table.Column<bool>(nullable: false),
                    ShowCredits = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSettings", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserSettings");
        }
    }
}

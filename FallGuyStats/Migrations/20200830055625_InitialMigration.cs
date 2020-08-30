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
                    Crowns = table.Column<int>(nullable: false)
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
                    RoundTypeId = table.Column<int>(nullable: false),
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

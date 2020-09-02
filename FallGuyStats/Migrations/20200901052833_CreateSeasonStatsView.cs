using Microsoft.EntityFrameworkCore.Migrations;

namespace FallGuyStats.Migrations
{
    public partial class CreateSeasonStatsView : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder
                .Sql(@"CREATE VIEW vSeasonStats as 
                        SELECT Season,
                        Count(id) as EpisodeCount,
                        sum(crowns) as CrownCount,
                        0 as CheaterCount,
                        0 as RoundsSinceCrown
                        FROM Episodes Group by season;");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP VIEW vSeasonStats;");
        }
    }
}

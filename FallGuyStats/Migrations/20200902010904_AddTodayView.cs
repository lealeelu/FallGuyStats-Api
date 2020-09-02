using Microsoft.EntityFrameworkCore.Migrations;

namespace FallGuyStats.Migrations
{
    public partial class AddTodayView : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder
                .Sql(@"CREATE VIEW vTodayStats as
                    SELECT Season, DATE(EpisodeFinished) as EpisodeFinishedDate, 
                    Count(id) as EpisodeCount, sum(crowns) as CrownCount
                    FROM Episodes
                    Group by Season, DATE(EpisodeFinished);");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP VIEW vTodayStats;");
        }
    }
}

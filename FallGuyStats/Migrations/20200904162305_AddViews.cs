using Microsoft.EntityFrameworkCore.Migrations;

namespace FallGuyStats.Migrations
{
    public partial class AddViews : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP VIEW vSinceWin;");
            migrationBuilder.Sql("DROP VIEW vSinceLoss;");
            migrationBuilder.Sql("DROP VIEW vRoundStats;");
            migrationBuilder.Sql("DROP VIEW vTodayStats;");
            migrationBuilder.Sql("DROP VIEW vSeasonStats;");

            migrationBuilder.Sql(@"CREATE View vSinceWin AS
                            select count(id)
                            from episodes
                            where crowns == 0
                            AND
                            created >
                            (select max(created)
                            from episodes
                            where crowns == 1
                            group by crowns
                            order by  max(created));");
            migrationBuilder.Sql(@"CREATE VIEW vSinceLoss as
                            select count(id)
                            from episodes
                            where crowns == 1
                            AND
                            created >
                            (select max(created)
                            from episodes
                            where crowns == 0
                            group by crowns
                            order by  max(created));");
            migrationBuilder
                .Sql(@"CREATE VIEW vRoundStats as
                    SELECT RoundType,
                    sum(CASE WHEN Badge is 'gold' THEN 1 else 0 end) as GoldCount,
                    sum(CASE WHEN Badge is 'silver' THEN 1 else 0 end) as SilverCount,
                    sum(CASE WHEN Badge is 'bronze' THEN 1 else 0 end) as BronzeCount,
                    sum(Qualified) as QualifiedCount,
                    sum(CASE WHEN Qualified == 1 THEN 0 else 1 end) as NotQualifiedCount
                    from Rounds
                    group by RoundType;");
            migrationBuilder
                .Sql(@"CREATE VIEW vTodayStats as
                    SELECT Season, DATE(EpisodeFinished) as EpisodeFinishedDate, 
                    Count(id) as EpisodeCount,
                    SUM(crowns) as CrownCount,
                    0 as Cheaters,
                    0 as RoundsSinceCrown
                    FROM Episodes
                    Group by Season, DATE(EpisodeFinished);");
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
            migrationBuilder.Sql("DROP VIEW vSinceWin;");
            migrationBuilder.Sql("DROP VIEW vSinceLoss;");
            migrationBuilder.Sql("DROP VIEW vRoundStats;");
            migrationBuilder.Sql("DROP VIEW vTodayStats;");
            migrationBuilder.Sql("DROP VIEW vSeasonStats;");
        }
    }
}

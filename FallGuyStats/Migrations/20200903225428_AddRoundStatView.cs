using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FallGuyStats.Migrations
{
    public partial class AddRoundStatView : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP VIEW vRoundStats");
        }
    }
}

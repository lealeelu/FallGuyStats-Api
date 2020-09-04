using Microsoft.EntityFrameworkCore;
using FallGuyStats.Models;
using FallGuyStats.Objects.Models.Views;

namespace FallGuyStats.Data
{
    public class FallGuysContext : DbContext
    {
        public FallGuysContext (DbContextOptions<FallGuysContext> options) : base (options)
        {

        }

        public DbSet<EpisodeModel> Episodes { get; set; }
        public DbSet<RoundModel> Rounds { get; set; }
        public DbSet<SeasonStatsView> SeasonStats { get; set; }
        public DbSet<TodayStatsView> TodayStats { get; set; }
        public DbSet<RoundStatsView> RoundStats { get; set; }
    }
}

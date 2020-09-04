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

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<RoundStatsView>(b =>
            {
                b.HasKey("RoundType");
                b.ToView("RoundStats");
            });

            modelBuilder.Entity<SeasonStatsView>(b =>
            {
                b.HasKey("Season");
                b.ToView("SeasonStats");
            });

            modelBuilder.Entity<TodayStatsView>(b =>
            {
                b.HasKey("EpisodeFinishedDate");
                b.ToView("TodayStats");
            });
        }

        public DbSet<EpisodeModel> Episodes { get; set; }
        public DbSet<RoundModel> Rounds { get; set; }
        public DbSet<SeasonStatsView> SeasonStats { get; set; }
        public DbSet<TodayStatsView> TodayStats { get; set; }
        public DbSet<RoundStatsView> RoundStats { get; set; }
    }
}

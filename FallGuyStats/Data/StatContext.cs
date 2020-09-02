using Microsoft.EntityFrameworkCore;
using FallGuyStats.Models;
using FallGuyStats.Objects.Models.Views;

namespace FallGuyStats.Data
{
    public class StatContext : DbContext
    {
        public StatContext(DbContextOptions<StatContext> options) : base (options)
        {
            Database.EnsureCreated();
        }

        public DbSet<SeasonStatsView> SeasonStats { get; set; }
        public DbSet<TodayStatsView> TodayStats { get; set; }

    }
}

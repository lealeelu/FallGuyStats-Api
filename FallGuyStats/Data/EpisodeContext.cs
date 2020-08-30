using Microsoft.EntityFrameworkCore;
using FallGuyStats.Models;

namespace FallGuyStats.Data
{
    public class EpisodeContext : DbContext
    {
        public EpisodeContext (DbContextOptions<EpisodeContext> options) : base (options)
        {

        }

        public DbSet<Episode> Episodes { get; set; }
        public DbSet<Round> Rounds { get; set; }

    }
}

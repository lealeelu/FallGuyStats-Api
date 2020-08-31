using Microsoft.EntityFrameworkCore;
using FallGuyStats.Models;

namespace FallGuyStats.Data
{
    public class EpisodeContext : DbContext
    {
        public EpisodeContext (DbContextOptions<EpisodeContext> options) : base (options)
        {

        }

        public DbSet<EpisodeModel> Episodes { get; set; }
        public DbSet<RoundModel> Rounds { get; set; }

    }
}

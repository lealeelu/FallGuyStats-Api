using FallGuyStats.Models;
using Microsoft.EntityFrameworkCore;

namespace FallGuyStats.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Episode> Episodes { get; set; }
        public DbSet<Round> Rounds { get; set; }
        public DbSet<RoundType> RoundTypes { get; set; }
    }

}
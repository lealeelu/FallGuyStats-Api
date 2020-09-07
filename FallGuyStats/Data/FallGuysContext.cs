// FallGuyStats Copyright (c) Leah Lee. All rights reserved.
// Licensed under the GNU General Public License v3.0
// Please Consider supporting the developer with some good good coffee: ko-fi.com/lealeelu

using Microsoft.EntityFrameworkCore;
using FallGuyStats.Models;
using FallGuyStats.Objects.Models.Views;

namespace FallGuyStats.Data
{
    public class FallGuysContext : DbContext
    {
        public FallGuysContext (DbContextOptions<FallGuysContext> options) : base (options) {}

        public DbSet<EpisodeModel> Episodes { get; set; }
        public DbSet<RoundModel> Rounds { get; set; }
    }
}

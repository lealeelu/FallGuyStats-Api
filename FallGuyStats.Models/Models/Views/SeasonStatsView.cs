using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FallGuyStats.Objects.Models.Views
{
 
    [Table("vSeasonStats")]
    public class SeasonStatsView
    {
        [Key]
        public int Season { get; set; }
        public int EpisodeCount { get; set; }
        public int CrownCount { get; set; }
    }
}

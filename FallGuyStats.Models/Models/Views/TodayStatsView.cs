using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FallGuyStats.Objects.Models.Views
{
    public class TodayStatsView
    {
        public int Season { get; set; }
        [Key]
        public DateTime EpisodeFinishedDate { get; set; }
        public int EpisodeCount { get; set; }
        public int CrownCount { get; set; }
    }
}

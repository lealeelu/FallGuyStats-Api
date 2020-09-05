using FallGuyStats.Objects.Models.Views;
using System.Collections.Generic;

namespace FallGuyStats.Objects.DTOs
{
    public class StatDTO
    {
        public SessionStatDTO TodayStats { get; set; }
        public SessionStatDTO SeasonStats { get; set; }
        public RoundStatsDto RoundStats { get; set; } 
        public string CurrentRound { get; set; }
        public StreakDto Streak { get; set; }
    }    
}

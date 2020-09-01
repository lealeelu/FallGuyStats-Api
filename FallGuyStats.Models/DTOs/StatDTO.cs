using System;
using System.Collections.Generic;
using System.Text;

namespace FallGuyStats.Objects.DTOs
{
    public class StatDTO
    {
        public SessionStatDTO TodayStats { get; set; }
        public SessionStatDTO SeasonStats { get; set; }
    }

    public class SessionStatDTO
    {
        public int Crowns { get; set; }
        public int Episodes { get; set; }
        public int Cheaters { get; set; }
        public int RoundsSinceCrown { get; set; }
    }
}

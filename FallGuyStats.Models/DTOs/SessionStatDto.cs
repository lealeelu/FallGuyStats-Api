using System;
using System.Collections.Generic;
using System.Text;

namespace FallGuyStats.Objects.DTOs
{
    public class SessionStatDTO
    {
        public int CrownCount { get; set; }
        public int EpisodeCount { get; set; }
        public int CheaterCount { get; set; }
        public int RoundsSinceCrown { get; set; }
    }
}

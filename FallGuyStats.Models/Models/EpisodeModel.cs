using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FallGuyStats.Models
{
    public class EpisodeModel
    {
        public int Id { get; set; }
        public int Kudos { get; set; }
        public int Fame { get; set; }
        public int Crowns { get; set; }
        public int RoundsPlayed { get; set; }

        public string Timestamp { get; set; }

        public DateTime Created { get; set; }

    }
}
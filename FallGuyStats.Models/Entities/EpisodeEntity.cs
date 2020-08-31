using System;
using System.Collections.Generic;
using System.Text;

namespace FallGuyStats.Objects.Entities
{
    public class EpisodeEntity
    {
        public int Id { get; set; }
        public int Kudos { get; set; }
        public int Fame { get; set; }
        public int Crowns { get; set; }
        public int RoundsPlayed { get; set; }
        public List<RoundEntity> RoundEntities { get; set; }

        public string Timestamp { get; set; }

        public DateTime Created { get; set; }

    }
}

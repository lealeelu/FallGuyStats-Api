// FallGuyStats Copyright (c) Leah Lee. All rights reserved.
// Licensed under the GNU General Public License v3.0
// Please Consider supporting the developer with some good good coffee: ko-fi.com/lealeelu

using System;
using System.Collections.Generic;
using System.Text;

namespace FallGuyStats.Objects.Entities
{
    public class EpisodeEntity
    {
        public EpisodeEntity()
        {
            RoundEntities = new List<RoundEntity>();
        }

        public int Id { get; set; }
        public int Kudos { get; set; }
        public int Fame { get; set; }
        public int Crowns { get; set; }
        public int RoundsPlayed { get; set; }
        public int Season { get; set; }
        public List<RoundEntity> RoundEntities { get; set; }

        public string Timestamp { get; set; }

        public DateTime EpisodeFinished { get; set; }
        public DateTime Created { get; set; }

    }
}

// FallGuyStats Copyright (c) Leah Lee. All rights reserved.
// Licensed under the GNU General Public License v3.0
// Please Consider supporting the developer with some good good coffee: ko-fi.com/lealeelu

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FallGuyStats.Objects.Models.Views
{
    public class SeasonStatsDto
    {
        public int Season { get; set; }
        public int CrownCount { get; set; }
        public int EpisodeCount { get; set; }
        public int CheaterCount { get; set;}
        public int RoundsSinceCrown { get; set; }
    }
}

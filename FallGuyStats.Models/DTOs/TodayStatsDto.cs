// FallGuyStats Copyright (c) Leah Lee. All rights reserved.
// Licensed under the GNU General Public License v3.0
// Please Consider supporting the developer with some good good coffee: ko-fi.com/lealeelu

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FallGuyStats.Objects.Models.Views
{
    public class TodayStatsDto
    {
        public int Season { get; set; }
        public DateTime EpisodeFinishedDate { get; set; }
        public int EpisodeCount { get; set; }
        public int CrownCount { get; set; }
    }
}

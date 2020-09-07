// FallGuyStats Copyright (c) Leah Lee. All rights reserved.
// Licensed under the GNU General Public License v3.0
// Please Consider supporting the developer with some good good coffee: ko-fi.com/lealeelu

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FallGuyStats.Models
{
    public class RoundModel
    {
        public int Id { get; set; }
        public int EpisodeId { get; set; }
        public string RoundType { get; set; }
        public bool Qualified { get; set; }
        public int Position { get; set; }
        public int Kudos { get; set; }
        public int Fame { get; set; }
        public int BonusTier { get; set; }
        public int BonusKudos { get; set; }
        public int BonusFame { get; set; }
        public string Badge { get; set; }

    }
}
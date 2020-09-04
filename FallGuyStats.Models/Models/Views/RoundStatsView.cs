using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FallGuyStats.Objects.Models.Views
{
    [Table("vRoundStats")]
    public class RoundStatsView
    {
        [Key]
        public string RoundType { get; set; }
        public int GoldCount { get; set; }
        public int SilverCount { get; set; }
        public int BronzeCount { get; set;}
        public int QualifiedCount { get; set; }
        public int NotQualifiedCount { get; set; }
    }
}

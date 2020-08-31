using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FallGuyStats.Models
{
    public class Round
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

        public static SortedDictionary<string, string> RoundTypeMap = new SortedDictionary<string, string>
        {
            { "gauntlet_01", "Hit Parade" },
            { "gauntlet_02", "Dizzy Heights" },
            { "gauntlet_03", "The Whirlygig" },
            { "match_fall", "Perfect Match" },
            { "chompchomp", "Gate Crash" },
            { "door_dash", "Door Dash" },
            { "tunnel", "Roll Out" },
            { "lava", "Slime Climb" },
            { "tip_toe", "Tip Toe" },
            { "floor_fall", "Hex-A-Gone" },
            { "dodge_fall", "Fruit Chute" },
            { "tail_tag", "Solo Tail Tag" },
            { "hoops", "Hoopsie Daisy" },
            { "jump_showdown", "Jump Showdown" },
            { "jump_club", "Jump Club" },
            { "conveyor_arena", "Team Tail Tag" },
            { "block_party", "Block Party" },
            { "fall_mountain_hub_complete", "Fall Mountain" },
            { "royal_rumble", "Royal Fumble" },
            { "fall_ball_60_players", "Fall Ball" },
            { "jinxed", "Jinxed" },
            { "egg_drop", "Egg Scramble" },
            { "ballhogs", "Hoarders" },
            { "rocknroll", "Rock'n'Roll" },
            { "see_saw", "See Saw" }
        };
    }
}
// FallGuyStats Copyright (c) Leah Lee. All rights reserved.
// Licensed under the GNU General Public License v3.0
// Please Consider supporting the developer with some good good coffee: ko-fi.com/lealeelu

using System;
using System.Collections.Generic;
using System.Text;

namespace FallGuyStats.Objects.Entities
{
    public class RoundEntity
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
        public int RoundsPlayed { get; set; }

        public static SortedDictionary<string, string> RoundTypeMap = new SortedDictionary<string, string>
        {
            { "round_gauntlet_01", "Hit Parade" },
            { "round_gauntlet_02", "Dizzy Heights" },
            { "round_gauntlet_03", "The Whirlygig" },
            { "round_match_fall", "Perfect Match" },
            { "round_chompchomp", "Gate Crash" },
            { "round_door_dash", "Door Dash" },
            { "round_tunnel", "Roll Out" },
            { "round_lava", "Slime Climb" },
            { "round_tip_toe", "Tip Toe" },
            { "round_floor_fall", "Hex-A-Gone" },
            { "round_dodge_fall", "Fruit Chute" },
            { "round_tail_tag", "Solo Tail Tag" },
            { "round_hoops", "Hoopsie Daisy" },
            { "round_jump_showdown", "Jump Showdown" },
            { "round_jump_club", "Jump Club" },
            { "round_conveyor_arena", "Team Tail Tag" },
            { "round_block_party", "Block Party" },
            { "round_fall_mountain_hub_complete", "Fall Mountain" },
            { "round_royal_rumble", "Royal Fumble" },
            { "round_fall_ball_60_players", "Fall Ball" },
            { "round_jinxed", "Jinxed" },
            { "round_egg_drop", "Egg Scramble" },
            { "round_ballhogs", "Hoarders" },
            { "round_rocknroll", "Rock'n'Roll" },
            { "round_see_saw", "See Saw" }
        };
    }
}

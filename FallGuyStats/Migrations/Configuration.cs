namespace FallGuyStats.Migrations
{
    using FallGuyStats.Models;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration 
    {
        public Configuration()
        {
            //AutomaticMigrationsEnabled = false;
        }
        /*
        protected override void Seed(FallGuyStats.Data.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.
            context.RoundTypes.AddOrUpdate(
                new RoundType { Id = 1, Code = "gauntlet_01", Name = "Hit Parade" },
                new RoundType { Id = 1, Code ="gauntlet_03", Name = "Whirlygig" },
                new RoundType { Id = 1, Code ="match_fall", Name = "Perfect Match" },
                new RoundType { Id = 1, Code ="chompchomp", Name = "DUNNO" },
                new RoundType { Id = 1, Code ="door_dash", Name = "Door Dash" },
                new RoundType { Id = 1, Code ="tunnel", Name = "Roll Out" },
                new RoundType { Id = 1, Code ="lava", Name = "Slime Climb" },
                new RoundType { Id = 1, Code ="tip_toe", Name = "Tip Toe" },
                new RoundType { Id = 1, Code ="floor_fall", Name = "Hex-A-Gone" },
                new RoundType { Id = 1, Code ="dodge_fall", Name = "Fruit Chute" },
                new RoundType { Id = 1, Code ="tail_tag", Name = "Solo Tail Tag" },
                new RoundType { Id = 1, Code ="hoops", Name = "Hoopsie Daisy" },
                new RoundType { Id = 1, Code ="jump_showdown", Name = "Jump Showdown" },
                new RoundType { Id = 1, Code ="conveyor_arena", Name = "Team Tail Tag" },
                new RoundType { Id = 1, Code ="block_party", Name = "Block Party" },
                new RoundType { Id = 1, Code ="unknown1", Name = "Fall Mountain" },
                new RoundType { Id = 1, Code ="unknown2", Name = "Royal Fumble" },
                new RoundType { Id = 1, Code ="unknown3", Name = "Fall Ball" },
                new RoundType { Id = 1, Code ="unknown4", Name = "Jinxed" },
                new RoundType { Id = 1, Code ="unknown5", Name = "Egg Scramble" },
                new RoundType { Id = 1, Code ="unknown6", Name = "Hoarders" },
                new RoundType { Id = 1, Code ="unknown7", Name = "Rock'n'Roll" },
                new RoundType { Id = 1, Code ="unknown8", Name = "See Saw" },
                new RoundType { Id = 1, Code ="unknown9", Name = "Dizzy Heights" },
                new RoundType { Id = 1, Code ="unknown10", Name = "Gate Crash" }
            );

            context.Rounds.AddAsync<Round>(
                new Round { Id = 0, Kudos = 100, Fame = 101, Badge = "gold", RoundTypeId = 1 }
            );

            context.Episodes.AddOrUpdate<Episode>(
                new Episode { Fame = 101, Id = 0, Crowns = 1, Kudos = 100, Rounds = new int[] { 1 } }
                );
    }*/
    }
}

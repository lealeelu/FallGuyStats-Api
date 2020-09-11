// FallGuyStats Copyright (c) Leah Lee. All rights reserved.
// Licensed under the GNU General Public License v3.0
// Please Consider supporting the developer with some good good coffee: ko-fi.com/lealeelu

using NUnit.Framework;
using FallGuyStats.Tools;
using System;

namespace UnitTests
{
    public class LogParserV2Test
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void V2ParseTest()
        {
            var ep = LogParserV2.GetEpisodesFromLogString(logSample);
            var expectedFinishDate = DateTime.Today.AddHours(23).AddMinutes(21).AddSeconds(39); 
            Assert.AreEqual(0, ep[0].Crowns);
            Assert.AreEqual(180, ep[0].Kudos);
            Assert.AreEqual(81, ep[0].Fame);
            Assert.IsNotNull(ep[0]);
            Assert.AreEqual(ep[0].EpisodeFinished, expectedFinishDate);
        }

        const string logSample = @"23:21:39.384: == [CompletedEpisodeDto] ==

> Kudos: 180

> Fame: 81

> Crowns: 0



[Round 0 | round_gauntlet_02]

> Qualified: True

> Position: 23

> Kudos: 30

> Fame: 15

> Bonus Tier: 2

> Bonus Kudos: 35

> Bonus Fame: 18

> BadgeId: bronze





[Round 1 | round_tunnel]

> Qualified: True

> Position: 8

> Kudos: 20

> Fame: 10

> Bonus Tier: 0

> Bonus Kudos: 35

> Bonus Fame: 18

> BadgeId: gold





[Round 2 | round_tip_toe]

> Qualified: False

> Position: 19

> Kudos: 60

> Fame: 20

> Bonus Tier: 

> Bonus Kudos: 0

> Bonus Fame: 0

> BadgeId: 



";
    }
}
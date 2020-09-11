using System;
using System.Collections.Generic;
using System.Linq;
using FallGuyStats.Data;
using FallGuyStats.Models;
using FallGuyStats.Objects.Entities;
using FallGuyStats.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace UnitTests
{
    public class EpisodeRepositoryTests
    {

        private Mock<ILogger<EpisodeRepository>> _logger;

        [SetUp]
        public void SetUp()
        {
            _logger = new Mock<ILogger<EpisodeRepository>>();
        }

        [Test]
        public void GetStreak_WhenWinning_ReturnsWinningStreakDto()
        {

            var episodeRepository = BuildRepository(new List<EpisodeModel>()
            {
                new EpisodeModel{Crowns = 0, Created = DateTime.Today.AddHours(1)},
                new EpisodeModel{Crowns = 1, Created = DateTime.Today.AddHours(2)},
                new EpisodeModel{Crowns = 0, Created = DateTime.Today.AddHours(3)},
                new EpisodeModel{Crowns = 0, Created = DateTime.Today.AddHours(4)},
                new EpisodeModel{Crowns = 1, Created = DateTime.Today.AddHours(5)},
                new EpisodeModel{Crowns = 1, Created = DateTime.Today.AddHours(6)},
            }, null);
            var streakDto = episodeRepository.GetStreak();
            Assert.IsTrue(streakDto.Winning);
            Assert.AreEqual(streakDto.Streak, 2);
        }

        [Test]
        public void GetStreak_WhenLosing_ReturnsLosingStreakDto()
        {
            var episodeRepository = BuildRepository(new List<EpisodeModel>()
            {
                new EpisodeModel{Crowns = 0, Created = DateTime.Today},
                new EpisodeModel{Crowns = 0, Created = DateTime.Today.AddHours(1)},
                new EpisodeModel{Crowns = 1, Created = DateTime.Today.AddHours(2)},
                new EpisodeModel{Crowns = 0, Created = DateTime.Today.AddHours(3)},
                new EpisodeModel{Crowns = 0, Created = DateTime.Today.AddHours(4)},
                new EpisodeModel{Crowns = 1, Created = DateTime.Today.AddHours(5)},
                new EpisodeModel{Crowns = 1, Created = DateTime.Today.AddHours(6)},
                new EpisodeModel{Crowns = 0, Created = DateTime.Today.AddHours(7)},
                new EpisodeModel{Crowns = 0, Created = DateTime.Today.AddHours(8)},
                new EpisodeModel{Crowns = 0, Created = DateTime.Today.AddHours(9)},
                new EpisodeModel{Crowns = 0, Created = DateTime.Today.AddHours(10)},

            }, null);
            var streakDto = episodeRepository.GetStreak();
            Assert.IsFalse(streakDto.Winning);
            Assert.AreEqual( 4, streakDto.Streak);
        }

        [Test]
        public void GetStreak_WhenNoEps_ReturnsWinningStreakDto()
        {
            var episodeRepository = BuildRepository();
            var streakDto = episodeRepository.GetStreak();
            Assert.IsTrue(streakDto.Winning);
            Assert.AreEqual(0, streakDto.Streak);
        }

        [Test]
        public void GetRoundStats_WhenMixedStats_ReturnCorrectCounts()
        {
            var roundType = "testRound";
            var notRoundType = "nottestRound";
            var repo = BuildRepository(null,
                new List<RoundModel>()
                {
                    new RoundModel(){RoundType = notRoundType, Badge = RoundModel.Gold, Qualified = true},
                    new RoundModel(){RoundType = roundType, Badge = RoundModel.Silver, Qualified = true},
                    new RoundModel(){RoundType = roundType, Badge = null, Qualified = false},
                    new RoundModel(){RoundType = roundType, Badge = null, Qualified = true},
                    new RoundModel(){RoundType = notRoundType, Badge = RoundModel.Bronze, Qualified = true},
                    new RoundModel(){RoundType = roundType, Badge = RoundModel.Bronze, Qualified = true},
                    new RoundModel(){RoundType = roundType, Badge = null, Qualified = true},
                    new RoundModel(){RoundType = notRoundType, Badge = null, Qualified = false},
                    new RoundModel(){RoundType = roundType, Badge = RoundModel.Silver, Qualified = true},
                    new RoundModel(){RoundType = roundType, Badge = null, Qualified = false},
                    new RoundModel(){RoundType = roundType, Badge = RoundModel.Gold, Qualified = true}
                });
            var roundStats = repo.GetRoundStats(roundType);
            Assert.AreEqual(1, roundStats.GoldCount);
            Assert.AreEqual(2, roundStats.SilverCount);
            Assert.AreEqual(1, roundStats.BronzeCount);
            Assert.AreEqual(6, roundStats.QualifiedCount);
            Assert.AreEqual(2, roundStats.NotQualifiedCount);
        }

        [Test]
        public void GetRoundStats_WhenNoStats_ReturnDefault()
        {
            var roundType = "testRound";
            var repo = BuildRepository(null, null);
            var roundStats = repo.GetRoundStats(roundType);
            Assert.AreEqual(0, roundStats.GoldCount);
            Assert.AreEqual(0, roundStats.SilverCount);
            Assert.AreEqual(0, roundStats.BronzeCount);
            Assert.AreEqual(0, roundStats.QualifiedCount);
            Assert.AreEqual(0, roundStats.NotQualifiedCount);
        }

        [Test]
        public void GetRoundStats_WhenOnlyWrongStats_ReturnDefault()
        {
            var roundType = "testRound";
            var notRoundType = "nottestRound";
            var repo = BuildRepository(null,
                new List<RoundModel>()
                {
                    new RoundModel(){RoundType = notRoundType, Badge = RoundModel.Gold, Qualified = true},
                    new RoundModel(){RoundType = notRoundType, Badge = RoundModel.Bronze, Qualified = true},
                    new RoundModel(){RoundType = notRoundType, Badge = null, Qualified = false},
                    new RoundModel(){RoundType = notRoundType, Badge = RoundModel.Gold, Qualified = true},
                    new RoundModel(){RoundType = notRoundType, Badge = RoundModel.Bronze, Qualified = true},
                    new RoundModel(){RoundType = notRoundType, Badge = null, Qualified = false},
                });
            var roundStats = repo.GetRoundStats(roundType);
            Assert.AreEqual(0, roundStats.GoldCount);
            Assert.AreEqual(0, roundStats.SilverCount);
            Assert.AreEqual(0, roundStats.BronzeCount);
            Assert.AreEqual(0, roundStats.QualifiedCount);
            Assert.AreEqual(0, roundStats.NotQualifiedCount);
        }

        private EpisodeRepository BuildRepository(List<EpisodeModel> episodeData = null, List<RoundModel> roundData = null)
        {
            if (episodeData == null) episodeData = new List<EpisodeModel>();
            if (roundData == null) roundData = new List<RoundModel>();

            var options = new DbContextOptionsBuilder<FallGuysContext>()
                        .Options;
            var mockContext = new Mock<FallGuysContext>(options);

            //episodes
            var episodes = episodeData.AsQueryable();
            var mockEpisodesSet = new Mock<DbSet<EpisodeModel>>();
            mockEpisodesSet.As<IQueryable<EpisodeModel>>().Setup(m => m.Provider).Returns(episodes.Provider);
            mockEpisodesSet.As<IQueryable<EpisodeModel>>().Setup(m => m.Expression).Returns(episodes.Expression);
            mockEpisodesSet.As<IQueryable<EpisodeModel>>().Setup(m => m.ElementType).Returns(episodes.ElementType);
            mockEpisodesSet.As<IQueryable<EpisodeModel>>().Setup(m => m.GetEnumerator()).Returns(episodes.GetEnumerator());

            mockContext.Setup(m => m.Episodes).Returns(mockEpisodesSet.Object);
            
            //rounds
            var rounds = roundData.AsQueryable();
            var mockRoundsSet = new Mock<DbSet<RoundModel>>();
            mockRoundsSet.As<IQueryable<RoundModel>>().Setup(m => m.Provider).Returns(rounds.Provider);
            mockRoundsSet.As<IQueryable<RoundModel>>().Setup(m => m.Expression).Returns(rounds.Expression);
            mockRoundsSet.As<IQueryable<RoundModel>>().Setup(m => m.ElementType).Returns(rounds.ElementType);
            mockRoundsSet.As<IQueryable<RoundModel>>().Setup(m => m.GetEnumerator()).Returns(rounds.GetEnumerator());

            mockContext.Setup(m => m.Rounds).Returns(mockRoundsSet.Object);
            
            return new EpisodeRepository(mockContext.Object, _logger.Object);
        }
    }
}

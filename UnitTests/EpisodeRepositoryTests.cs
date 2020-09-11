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
            var episodes = new List<EpisodeModel>()
            {
                new EpisodeModel{Crowns = 0, Created = DateTime.Today.AddHours(1)},
                new EpisodeModel{Crowns = 1, Created = DateTime.Today.AddHours(2)},
                new EpisodeModel{Crowns = 0, Created = DateTime.Today.AddHours(3)},
                new EpisodeModel{Crowns = 0, Created = DateTime.Today.AddHours(4)},
                new EpisodeModel{Crowns = 1, Created = DateTime.Today.AddHours(5)},
                new EpisodeModel{Crowns = 1, Created = DateTime.Today.AddHours(6)},
            }.AsQueryable();
            var mockSet = new Mock<DbSet<EpisodeModel>>();
            mockSet.As<IQueryable<EpisodeModel>>().Setup(m => m.Provider).Returns(episodes.Provider);
            mockSet.As<IQueryable<EpisodeModel>>().Setup(m => m.Expression).Returns(episodes.Expression);
            mockSet.As<IQueryable<EpisodeModel>>().Setup(m => m.ElementType).Returns(episodes.ElementType);
            mockSet.As<IQueryable<EpisodeModel>>().Setup(m => m.GetEnumerator()).Returns(episodes.GetEnumerator());

            var options = new DbContextOptionsBuilder<FallGuysContext>()
                    .Options;

            var mockContext = new Mock<FallGuysContext>(options);
            mockContext.Setup(m => m.Episodes).Returns(mockSet.Object);
            var episodeRepository = new EpisodeRepository(mockContext.Object, _logger.Object);
            var streakDto = episodeRepository.GetStreak();
            Assert.IsTrue(streakDto.Winning);
            Assert.AreEqual(streakDto.Streak, 2);
        }

        [Test]
        public void GetStreak_WhenLosing_ReturnsLosingStreakDto()
        {
            var episodes = new List<EpisodeModel>()
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

            }.AsQueryable();
            var mockSet = new Mock<DbSet<EpisodeModel>>();
            mockSet.As<IQueryable<EpisodeModel>>().Setup(m => m.Provider).Returns(episodes.Provider);
            mockSet.As<IQueryable<EpisodeModel>>().Setup(m => m.Expression).Returns(episodes.Expression);
            mockSet.As<IQueryable<EpisodeModel>>().Setup(m => m.ElementType).Returns(episodes.ElementType);
            mockSet.As<IQueryable<EpisodeModel>>().Setup(m => m.GetEnumerator()).Returns(episodes.GetEnumerator());

            var options = new DbContextOptionsBuilder<FallGuysContext>()
                    .Options;

            var mockContext = new Mock<FallGuysContext>(options);
            mockContext.Setup(m => m.Episodes).Returns(mockSet.Object);
            var episodeRepository = new EpisodeRepository(mockContext.Object, _logger.Object);
            var streakDto = episodeRepository.GetStreak();
            Assert.IsFalse(streakDto.Winning);
            Assert.AreEqual(streakDto.Streak, 4);
        }

        [Test]
        public void GetStreak_WhenNoEps_ReturnsWinningStreakDto()
        {
            var episodes = new List<EpisodeModel>().AsQueryable();
            var mockSet = new Mock<DbSet<EpisodeModel>>();
            mockSet.As<IQueryable<EpisodeModel>>().Setup(m => m.Provider).Returns(episodes.Provider);
            mockSet.As<IQueryable<EpisodeModel>>().Setup(m => m.Expression).Returns(episodes.Expression);
            mockSet.As<IQueryable<EpisodeModel>>().Setup(m => m.ElementType).Returns(episodes.ElementType);
            mockSet.As<IQueryable<EpisodeModel>>().Setup(m => m.GetEnumerator()).Returns(episodes.GetEnumerator());

            var options = new DbContextOptionsBuilder<FallGuysContext>().Options;

            var mockContext = new Mock<FallGuysContext>(options);
            mockContext.Setup(m => m.Episodes).Returns(mockSet.Object);
            var episodeRepository = new EpisodeRepository(mockContext.Object, _logger.Object);
            var streakDto = episodeRepository.GetStreak();
            Assert.IsTrue(streakDto.Winning);
            Assert.AreEqual(streakDto.Streak, 0);
        }
    }
}

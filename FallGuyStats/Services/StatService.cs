using FallGuyStats.Models;
using Microsoft.Extensions.Logging;
using FallGuyStats.Tools;
using FallGuyStats.Data;
using Microsoft.EntityFrameworkCore.Internal;
using System.Linq;
using System;
using FallGuyStats.Objects.DTOs;
using FallGuyStats.Controllers;

namespace FallGuyStats.Services
{
    public class StatService
    {
        private readonly FallGuysContext _fallGuysContext;
        private readonly ILogger<StatService> _logger;

        public StatService (
            FallGuysContext episodeContext,
            ILogger<StatService> logger)
        {
            _fallGuysContext = episodeContext;
            _logger = logger;
        }

        public StatDTO GetStats()
        {
            CheckPlayerLog();
            var result = new StatDTO() { };
            var todayStat = _fallGuysContext.TodayStats
                .Where(s => s.EpisodeFinishedDate.Date == DateTime.Now.Date)
                .FirstOrDefault();            
            result.TodayStats = new SessionStatDTO
            {
                CrownCount = todayStat?.CrownCount ?? 0,
                EpisodeCount = todayStat?.EpisodeCount ?? 0,
                // TODO add cheater count
                CheaterCount = 0,
                // TODO add rounds since crown
                RoundsSinceCrown = 0
            };
            var seasonStat = _fallGuysContext.SeasonStats.Where(s => s.Season == 1)
                .FirstOrDefault();
            result.SeasonStats = new SessionStatDTO
            {
                CrownCount = seasonStat?.CrownCount ?? 0,
                EpisodeCount = seasonStat?.EpisodeCount ?? 0,
                // TODO add cheater count
                CheaterCount = 0,
                // TODO add rounds since crown
                RoundsSinceCrown = 0
            };
            return result;
        }

        // Check to see if there are any new eps and adds it to db
        public void CheckPlayerLog()
        {
            var newEpisodes = LogParserV2.GetEpisodesFromLog();
            foreach (var newEpisode in newEpisodes)
            {
                //check timestamps against db to determine if it is actually a new episode
                if (_fallGuysContext.Episodes.Any(episode => episode.Timestamp == newEpisode.Timestamp)) return;

                //convert entity to model
                var episodeModel = new EpisodeModel
                {
                    Crowns = newEpisode.Crowns,
                    Fame = newEpisode.Fame,
                    Kudos = newEpisode.Kudos,
                    Timestamp = newEpisode.Timestamp,
                    Season = 1,
                    EpisodeFinished = newEpisode.EpisodeFinished,
                    Created = DateTime.UtcNow
                };

                //add episode model to db
                var newEpisodeEntity = _fallGuysContext.Episodes.Add(episodeModel);
                _fallGuysContext.SaveChanges();
                //after the episode has been commited to the db, get the new episode id
                var id = _fallGuysContext.Episodes.SingleOrDefault(e => e.Timestamp == newEpisode.Timestamp).Id;
                    
                //add round models to db
                foreach (var round in newEpisode.RoundEntities)
                {
                    var roundModel = new RoundModel()
                    {
                        Kudos = round.Kudos,
                        Fame = round.Fame,
                        RoundType = round.RoundType,
                        Badge = round.Badge,
                        BonusFame = round.BonusFame,
                        BonusKudos = round.BonusKudos,
                        BonusTier = round.BonusTier,
                        EpisodeId = id,
                        Position = round.Position,
                        Qualified = round.Qualified
                    };
                    _fallGuysContext.Rounds.Add(roundModel);
                }
                _fallGuysContext.SaveChanges();
                _logger.LogInformation($"Saved {newEpisode.Timestamp} to db");
            }
        }
    }
}

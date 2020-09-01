using FallGuyStats.Models;
using Microsoft.Extensions.Logging;
using FallGuyStats.Tools;
using FallGuyStats.Data;
using Microsoft.EntityFrameworkCore.Internal;
using System.Linq;
using System.Threading;
using System;
using FallGuyStats.Objects.Entities;
using FallGuyStats.Objects.DTOs;

namespace FallGuyStats.Services
{
    public class StatService
    {
        private readonly EpisodeContext _episodeContext;
        private readonly ILogger<StatService> _logger;

        public StatService (
            EpisodeContext episodeContext,
            ILogger<StatService> logger)
        {
            _episodeContext = episodeContext;
            _logger = logger;
        }

        public StatDTO GetStats()
        {
            CheckPlayerLog();
            var result = new StatDTO();
            return result;
        }

        // Check to see if there are any new eps and adds it to db
        public void CheckPlayerLog()
        {
            var newEpisodes = LogParser.GetEpisodesFromLog();
            foreach (var newEpisode in newEpisodes)
            {
                //check timestamps against db to determine if it is actually a new episode
                if (_episodeContext.Episodes.Any(episode => episode.Timestamp == newEpisode.Timestamp)) return;

                //convert entity to model
                var episodeModel = new EpisodeModel
                {
                    Crowns = newEpisode.Crowns,
                    Fame = newEpisode.Fame,
                    Kudos = newEpisode.Kudos,
                    Timestamp = newEpisode.Timestamp,
                    Season = newEpisode.Season,
                    Created = DateTime.UtcNow
                };

                //add episode model to db
                var newEpisodeEntity = _episodeContext.Episodes.Add(episodeModel);
                _episodeContext.SaveChanges();
                //after the episode has been commited to the db, get the new episode id
                var id = _episodeContext.Episodes.SingleOrDefault(e => e.Timestamp == newEpisode.Timestamp).Id;
                    
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
                    _episodeContext.Rounds.Add(roundModel);
                }
                _episodeContext.SaveChanges();
                _logger.LogInformation($"Saved {newEpisode.Timestamp} to db");
            }
        }
    }
}

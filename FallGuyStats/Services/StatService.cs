// FallGuyStats Copyright (c) Leah Lee. All rights reserved.
// Licensed under the GNU General Public License v3.0
// Please Consider supporting the developer with some good good coffee: ko-fi.com/lealeelu

using FallGuyStats.Models;
using Microsoft.Extensions.Logging;
using FallGuyStats.Tools;
using FallGuyStats.Data;
using Microsoft.EntityFrameworkCore.Internal;
using System.Linq;
using System;
using FallGuyStats.Objects.DTOs;
using FallGuyStats.Controllers;
using FallGuyStats.Repositories;
using System.Collections.Generic;
using FallGuyStats.Objects.Entities;

namespace FallGuyStats.Services
{
    public class StatService
    {
        private readonly EpisodeRepository _episodeRepository;
        private readonly ILogger<StatService> _logger;

        public StatService (
            EpisodeRepository episodeRepository,
            ILogger<StatService> logger)
        {
            _episodeRepository = episodeRepository;
            _logger = logger;
        }

        public StatDTO GetStats()
        {
            CheckPlayerLog();
            var result = new StatDTO() { };
            var todayStat = _episodeRepository.GetTodayStats();
            result.TodayStats = new SessionStatDTO
            {
                CrownCount = todayStat?.CrownCount ?? 0,
                EpisodeCount = todayStat?.EpisodeCount ?? 0,
                // TODO add cheater count
                CheaterCount = 0,
                // TODO add rounds since crown
                RoundsSinceCrown = 0
            };
            var seasonStat = _episodeRepository.GetSeasonStats(1);
            result.SeasonStats = new SessionStatDTO
            {
                CrownCount = seasonStat?.CrownCount ?? 0,
                EpisodeCount = seasonStat?.EpisodeCount ?? 0,
                // TODO add cheater count
                CheaterCount = 0,
                // TODO add rounds since crown
                RoundsSinceCrown = 0
            };
            //_logger.LogInformation($"Current Round: {LogParserV2.currentRound}");
            result.RoundStats = _episodeRepository.GetRoundStats(LogParserV2.currentRound);
            string readableRoundType;
            if (RoundEntity.RoundTypeMap.TryGetValue(LogParserV2.currentRound, out readableRoundType))
            {
                result.CurrentRound = readableRoundType;
            }
            result.Streak = _episodeRepository.GetStreak();
            return result;
        }

        // Check to see if there are any new eps and adds it to db
        public void CheckPlayerLog()
        {
            var newEpisodes = LogParserV2.GetEpisodesFromLog();
            if (newEpisodes == null) return;
            foreach (var newEpisode in newEpisodes)
            {
                //check timestamps against db to determine if it is actually a new episode
                if (_episodeRepository.EpisodeExists(newEpisode.Timestamp)) continue;
                
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

                var id = _episodeRepository.AddEpisode(episodeModel);

                //add round models to db
                var rounds = new List<RoundModel>();
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
                    rounds.Add(roundModel);
                }
                _episodeRepository.AddRounds(rounds);
                _logger.LogInformation($"Saved {newEpisode.Timestamp} to db");
            }
        }
    }
}

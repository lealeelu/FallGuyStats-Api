// FallGuyStats Copyright (c) Leah Lee. All rights reserved.
// Licensed under the GNU General Public License v3.0
// Please Consider supporting the developer with some good good coffee: ko-fi.com/lealeelu

using FallGuyStats.Data;
using FallGuyStats.Models;
using FallGuyStats.Objects.DTOs;
using FallGuyStats.Objects.Models.Views;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FallGuyStats.Repositories
{

    public class EpisodeRepository {

        private FallGuysContext _fallGuysContext;
        private ILogger<EpisodeRepository> _logger;

        public EpisodeRepository(
            FallGuysContext fallGuysContext,
            ILogger<EpisodeRepository> logger
        ) {
            _fallGuysContext = fallGuysContext;
            _logger = logger;
        }

        public int AddEpisode(EpisodeModel episode)
        {
            //add episode model to db
            var newEpisodeEntity = _fallGuysContext.Episodes.Add(episode);
            _fallGuysContext.SaveChanges();
            //after the episode has been commited to the db, get the new episode id
            return _fallGuysContext.Episodes.SingleOrDefault(e => e.Timestamp == episode.Timestamp).Id;
        }

        public void AddRounds(List<RoundModel> rounds)
        {
            foreach (var round in rounds)
            {
                _fallGuysContext.Rounds.Add(round);
            }
            _fallGuysContext.SaveChanges();
        }

        public bool EpisodeExists(string timestamp)
        {
            return _fallGuysContext.Episodes.Any(episode => episode.Timestamp == timestamp);
        }

        public TodayStatsDto GetTodayStats()
        {
            return _fallGuysContext.Episodes.Where(e => e.EpisodeFinished.Date == DateTime.Now.Date)
                .GroupBy(e => e.EpisodeFinished.Date)
                .Select(s => new TodayStatsDto
                {
                    EpisodeFinishedDate = s.Key,
                    EpisodeCount = s.Count(),
                    CrownCount = s.Sum(c => c.Crowns)                    
                })
                .FirstOrDefault();
        }

        public SeasonStatsDto GetSeasonStats(int season)
        {
            return _fallGuysContext.Episodes.Where(e => e.Season == season)
                .GroupBy(e => e.Season)
                .Select(s => new SeasonStatsDto
                {
                    Season = s.Key,
                    EpisodeCount = s.Count(),
                    CrownCount = s.Sum(c => c.Crowns),
                    CheaterCount = 0 
                })
                .FirstOrDefault();
        }

        public RoundStatsDto GetRoundStats(string roundName)
        {
            var roundStats = _fallGuysContext.Rounds
                .Where(round => round.RoundType == roundName)
                .GroupBy(r => r.RoundType)
                .Select(s => new RoundStatsDto
                {
                    RoundType = s.Key,
                    GoldCount = s.Sum(r => r.Badge == RoundModel.Gold ? 1 : 0),
                    SilverCount = s.Sum(r => r.Badge == RoundModel.Silver ? 1 : 0),
                    BronzeCount = s.Sum(r => r.Badge == RoundModel.Bronze ? 1 : 0),
                    QualifiedCount = s.Sum(r => r.Qualified ? 1 : 0),
                    NotQualifiedCount = s.Sum(r => r.Qualified ? 0 : 1)
                })
                .FirstOrDefault();
            if (roundStats == null)
            {
                roundStats = new RoundStatsDto()
                {
                    RoundType = roundName,
                    GoldCount = 0,
                    SilverCount = 0,
                    BronzeCount = 0,
                    QualifiedCount = 0,
                    NotQualifiedCount = 0
                };
            }
            return roundStats;
        }

        public StreakDto GetStreak()
        {
            var result = new StreakDto();
            try
            {
                var lastEpisode = _fallGuysContext.Episodes
                .OrderBy(e => e.Created)
                .LastOrDefault();
                if (lastEpisode == null)
                {
                    result.Winning = true;
                    result.Streak = 0;
                }
                else if (lastEpisode.Crowns > 0)
                {
                    result.Winning = true;
                    var maxLossCreated = _fallGuysContext.Episodes
                        .Where(e => e.Crowns == 0)
                        .GroupBy(e => e.Crowns)
                        .Select(s => s.Max(e => e.Created))
                        .FirstOrDefault();

                    result.Streak = _fallGuysContext.Episodes
                        .Where(e => e.Created > maxLossCreated)
                        .GroupBy(e => e.Crowns)
                        .Select(s => s.Count())
                        .FirstOrDefault();
                }
                else
                {
                    result.Winning = false;
                    var maxWinCreated = _fallGuysContext.Episodes
                        .Where(e => e.Crowns == 1)
                        .GroupBy(e => e.Crowns)
                        .Select(s => s.Max(e => e.Created))
                        .FirstOrDefault();

                    result.Streak = _fallGuysContext.Episodes
                        .Where(e => e.Created > maxWinCreated)
                        .GroupBy(e => e.Crowns)
                        .Select(s => s.Count())
                        .FirstOrDefault();
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Unable to get streak {e.Message}");
            }            

            return result;
        }
    }
}

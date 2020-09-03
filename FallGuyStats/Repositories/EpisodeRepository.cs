using FallGuyStats.Data;
using FallGuyStats.Models;
using FallGuyStats.Objects.Models.Views;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FallGuyStats.Repositories
{
    
    public class EpisodeRepository {

        private FallGuysContext _fallGuysContext;
        public EpisodeRepository(
            FallGuysContext fallGuysContext
        ) {
            _fallGuysContext = fallGuysContext;    
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

        public TodayStatsView GetTodayStats()
        {
            return _fallGuysContext.TodayStats
                .Where(s => s.EpisodeFinishedDate.Date == DateTime.Now.Date)
                .FirstOrDefault();
        }

        public SeasonStatsView GetSeasonStats()
        {
            return _fallGuysContext.SeasonStats.Where(s => s.Season == 1)
                .FirstOrDefault();
        }
    }
}

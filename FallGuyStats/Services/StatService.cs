using FallGuyStats.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using FallGuyStats.Tools;
using FallGuyStats.Data;
using FallGuyStats.Objects.Entities;

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

        // Check to see if there are any new eps and adds it to db
        public void CheckPlayerLog()
        {
            var newEpisodes = LogParser.GetEpisodesFromLog();
            foreach (var episode in newEpisodes)
            {
                //check timestamps against db to determine if it is actually a new episode

                //convert entity to model
                var episodeModel = new EpisodeModel();
                episodeModel.Crowns = episode.Crowns;
                //.....
                
                //add episode model to db
                _episodeContext.Episodes.Add(episodeModel);
                
                //add round models to db
                foreach (var round in episode.RoundEntities)
                {
                    var roundModel = new RoundModel();
                    roundModel.EpisodeId = episode.Id;
                    roundModel.Kudos = round.Kudos;
                    _episodeContext.Rounds.Add(roundModel);
                }
            }            
        }
    }
}

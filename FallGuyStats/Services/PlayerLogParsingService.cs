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
    public class PlayerLogParsingService
    {
        private readonly IFileProvider _fileProvider;
        private readonly EpisodeContext _episodeContext;
        private readonly ILogger<PlayerLogParsingService> _logger;

        private string playerLogFileLocation;
        public PlayerLogParsingService (
            IConfiguration configuration,
            EpisodeContext episodeContext,
            ILogger<PlayerLogParsingService> logger)
        {
            //_fileProvider = new PhysicalFileProvider(playerLogFileLocation);
            _episodeContext = episodeContext;
            _logger = logger;

            playerLogFileLocation = configuration.GetValue<string>("PlayerLogFileLocation");

            //RunFileWatch();
        }

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

        private void RunFileWatch()
        {
            //check if file exists first
            var fileInfo = _fileProvider.GetFileInfo(playerLogFileLocation);
            if (!fileInfo.Exists)
            {
                _logger.LogError($"Couldn't find player log file at: {playerLogFileLocation}\nCheck appsettings.json PlayerLogFileLocation");
                return;
            }           

            return;
        }
    }
}

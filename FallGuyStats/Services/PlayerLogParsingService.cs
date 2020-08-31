﻿using FallGuyStats.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using FallGuyStats.Tools;
using FallGuyStats.Data;

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
            Episode newEpisode = LogParser.GetEpisodeFromLog();
            if (newEpisode != null)
            {
                _episodeContext.Add(newEpisode);
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

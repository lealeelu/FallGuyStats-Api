using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;

namespace FallGuyStats.Services
{
    public class PlayerLogParsingService
    {
        private readonly IFileProvider _fileProvider;
        private readonly ILogger<PlayerLogParsingService> _logger;

        private string playerLogFileLocation;
        public PlayerLogParsingService (
            IConfiguration configuration,
            ILogger<PlayerLogParsingService> logger)
        {
            _fileProvider = new PhysicalFileProvider(playerLogFileLocation);
            _logger = logger;

            playerLogFileLocation = configuration.GetValue<string>("PlayerLogFileLocation");

            RunFileWatch();
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

            //watch that file
            _fileProvider.Watch(playerLogFileLocation).RegisterChangeCallback(
                file =>
                {
                    _logger.LogInformation("Change Detected");
                    // read file
                    // check if there is a new DTO detected via timestamp
                    // parse DTO and store in DB
                    // store new data in DB as an episode
                }, null);
            
            return;
        }
    }
}

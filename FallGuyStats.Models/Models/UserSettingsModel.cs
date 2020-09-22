namespace FallGuyStats.Objects.Models
{
    public class UserSettingsModel
    {
        public int Id { get; set; }
        public int PollingFrequency { get; set; }
        public bool ShowLastEpisode { get; set; }
        public bool ShowLosingStreak { get; set; }
        public bool ShowCheaterCount { get; set; }
        public bool ShowCredits { get; set; }

    }
}

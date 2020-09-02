using FallGuyStats.Objects.Entities;
using FallGuyStats.Tools;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FallGuysStats.Desktop
{
    /// <summary>
    /// Interaction logic for AllEpisodes.xaml
    /// </summary>
    public partial class AllEpisodesWindow : Window
    {
        private List<EpisodeEntity> Episodes
        { get; set; }

        private int EpisodeCount
        { get; set; }

        private int DisplayedEpisode
        { get; set; } = 1;

        private void UpdateEpisodeDisplay(int amount)
        {
            DisplayedEpisode = DisplayedEpisode + amount;
            tbEpisodeNumber.Text = $"Episode {DisplayedEpisode} of {EpisodeCount}";
            boxEpisodeData.Text += $"Rounds Played: {Episodes[DisplayedEpisode - 1].RoundsPlayed}\r\n";
            boxEpisodeData.Text += $"Total Kudos: {Episodes[DisplayedEpisode - 1].Kudos}\r\n";
            boxEpisodeData.Text += $"Total Fame: {Episodes[DisplayedEpisode - 1].Fame}\r\n";
            boxEpisodeData.Text += $"Crowns: {Episodes[DisplayedEpisode - 1].Crowns}\r\n";
            boxEpisodeData.Text += $"\r\n";
            int roundNumber = 1;
            foreach (RoundEntity round in Episodes[DisplayedEpisode - 1].RoundEntities)
            {
                boxEpisodeData.Text += $"Round {roundNumber}\r\n";
                boxEpisodeData.Text += $"Game: {round.RoundType}\r\n";
                boxEpisodeData.Text += $"Qualified: {round.Qualified}\r\n";
                boxEpisodeData.Text += $"Badge: {round.Badge}\r\n";
                boxEpisodeData.Text += $"Kudos: {round.Kudos}\r\n";
                boxEpisodeData.Text += $"Fame: {round.Fame}\r\n";
                boxEpisodeData.Text += $"\r\n";
                roundNumber++;
            }
        }

        public AllEpisodesWindow()
        {
            InitializeComponent();
            Episodes = LogParser.GetEpisodesFromLog();
            EpisodeCount = Episodes.Count;
            UpdateEpisodeDisplay(0);
            if (EpisodeCount == 1)
            {
                btnPrevious.IsEnabled = false;
                btnNext.IsEnabled = false;
            }
        }

        private void btnPrevious_Click(object sender, RoutedEventArgs e)
        {
            UpdateEpisodeDisplay(-1);
            btnNext.IsEnabled = true;
            if (DisplayedEpisode == 1)
            {
                btnPrevious.IsEnabled = false;
            }
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            UpdateEpisodeDisplay(1);
            btnPrevious.IsEnabled = true;
            if (DisplayedEpisode == EpisodeCount)
            {
                btnNext.IsEnabled = false;
            }
        }
    }
}

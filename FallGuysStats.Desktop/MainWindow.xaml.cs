using FallGuyStats.Models;
using FallGuyStats.Objects.Entities;
using FallGuyStats.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FallGuysStats.Desktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var currentUserAppData = System.Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            PlayerLogDataPath = currentUserAppData + "Low\\Mediatonic\\FallGuys_client\\Player.log";
        }

        CancellationTokenSource cts = new CancellationTokenSource();

        public string PlayerLogDataPath
        { get; set; }

        public List<string> PlayerLogData
        { get; set; }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var logData = LogParser.ReadLogData();
            var latestEpisodeData = LogParser.GetNewestEpisodeData(logData);
            if (latestEpisodeData != "")
            {
                PrimaryTextBox.Text = latestEpisodeData;
                btnLatestEpStats.IsEnabled = true;
                btnRoundData.IsEnabled = true;
                cbRound.IsEnabled = true;

                int searchIndex = 0;
                int roundCount = 0;
                cbRound.Items.Add("All");
                while ((searchIndex = latestEpisodeData.IndexOf("[Round", searchIndex)) != -1)
                {
                    roundCount++;
                    cbRound.Items.Add($"{roundCount}");
                    searchIndex++;
                }
                cbRound.SelectedIndex = 0;
            }
        }

        private void btnLatestEpStats_Click(object sender, RoutedEventArgs e)
        {
            boxResults.Text = "";
            var latestEpisodeData = PrimaryTextBox.Text;
            var latestEpisode = LogParser.GetEpisodeStats(latestEpisodeData);

            boxResults.Text += $"Kudos Earned: {latestEpisode.Kudos}\r\n";
            boxResults.Text += $"Fame Earned: {latestEpisode.Fame}\r\n";
            boxResults.Text += $"Crowns Earned: {latestEpisode.Crowns}\r\n";
            boxResults.Text += $"Rounds Played: {latestEpisode.RoundsPlayed}\r\n";
        }

        private void btnRoundData_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(cbRound.SelectedItem.ToString()))
            {
                boxResults.Text = "Invalid round selection";
            }
            else
            {
                boxResults.Text = "";
                if (cbRound.SelectedItem.ToString() == "All")
                {
                    var latestEpisodeData = PrimaryTextBox.Text;
                    var searchIndex = 0;
                    var roundCount = 0;
                    var resultsAllRounds = new List<RoundEntity>();
                    while ((searchIndex = latestEpisodeData.IndexOf("[Round", searchIndex)) != -1)
                    {
                        var currentRound = LogParser.GetRoundStats(latestEpisodeData, roundCount);
                        roundCount++;
                        searchIndex++;
                        resultsAllRounds.Add(currentRound);
                    }
                    roundCount = 1;
                    foreach (var roundResult in resultsAllRounds)
                    {
                        DisplayRoundData(roundResult, roundCount);
                        roundCount++;
                    }
                }
                else
                {
                    var roundNumber = Int32.Parse(cbRound.SelectedItem.ToString()) - 1;
                    var roundResult = LogParser.GetRoundStats(PrimaryTextBox.Text, roundNumber);
                    DisplayRoundData(roundResult, roundNumber);
                }
            }
        }
        private void DisplayRoundData(RoundEntity roundResult, int roundNumber)
        {
            boxResults.Text += $"Round {roundNumber}\r\n";
            boxResults.Text += $"Round Type: {roundResult.RoundType}\r\n";
            boxResults.Text += $"Qualified: {roundResult.Qualified}\r\n";
            boxResults.Text += $"Kudos Earned: {roundResult.Kudos}\r\n";
            boxResults.Text += $"Badge Earned: {roundResult.Badge}\r\n";
            boxResults.Text += $"\r\n";
        }

        private async void btnAutoMonitor_Click(object sender, RoutedEventArgs e)
        {

            if (tbStatus.Text == "Currently running")
            {
                cts.Cancel();
                tbStatus.Text = "Not running";
                btnAutoMonitor.Content = "Start Monitoring";
                btnLatestEpStats.IsEnabled = true;
                btnRoundData.IsEnabled = true;
                cbRound.IsEnabled = true;
            }
            else
            {
                // Check to see if all episode data in log currently is accounted for
                if (File.Exists(PlayerLogDataPath))
                {
                    tbStatus.Text = "Currently running";
                    btnAutoMonitor.Content = "Stop Monitoring";
                    btnLatestEpStats.IsEnabled = false;
                    btnRoundData.IsEnabled = false;
                    cbRound.IsEnabled = false;
                    LogParser.GetEpisodesFromLog();
                    boxResults.Text = await LogMonitor.ReadTail(PlayerLogDataPath, cts.Token);
                }
                else
                {
                    tbStatus.Text = "No player.log exists! Start playing Fall Guys first, you jabroni!";
                    // do nothing, show an error or something, need player data to work
                }
            }
        }

        private void btnAllEpisodes_Click(object sender, RoutedEventArgs e)
        {
            if (IsWindowOpen<AllEpisodesWindow>())
            {
                AllEpisodesWindow openWindow = Application.Current.Windows.OfType<AllEpisodesWindow>().First();
                openWindow.Activate();
            }
            else
            {
                if (File.Exists(PlayerLogDataPath))
                {
                    AllEpisodesWindow episodesWindow = new AllEpisodesWindow();
                    episodesWindow.Show();
                }
                else
                {

                }
            }
        }

        public static bool IsWindowOpen<T>(string name = "") where T : Window
        {
            return string.IsNullOrEmpty(name)
               ? Application.Current.Windows.OfType<T>().Any()
               : Application.Current.Windows.OfType<T>().Any(w => w.Name.Equals(name));
        }
    }
}

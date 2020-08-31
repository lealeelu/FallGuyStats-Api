using FallGuyStats.Models;
using FallGuyStats.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            List<string> logData = LogParser.ReadLogData();
            string latestEpisodeData = LogParser.GetNewestEpisodeData(logData);
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
            string latestEpisodeData = PrimaryTextBox.Text;
            Episode latestEpisode = LogParser.GetLastEpisodeStats(latestEpisodeData);

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
                    string latestEpisodeData = PrimaryTextBox.Text;
                    int searchIndex = 0;
                    int roundCount = 0;
                    List<Round> resultsAllRounds = new List<Round>();
                    while ((searchIndex = latestEpisodeData.IndexOf("[Round", searchIndex)) != -1)
                    {
                        Round currentRound = LogParser.GetRoundStats(latestEpisodeData, roundCount);
                        roundCount++;
                        searchIndex++;
                        resultsAllRounds.Add(currentRound);
                    }
                    roundCount = 1;
                    foreach (Round roundResult in resultsAllRounds)
                    {
                        DisplayRoundData(roundResult, roundCount);
                        roundCount++;
                    }
                }
                else
                {
                    int roundNumber = Int32.Parse(cbRound.SelectedItem.ToString()) - 1;
                    Round roundResult = LogParser.GetRoundStats(PrimaryTextBox.Text, roundNumber);
                    DisplayRoundData(roundResult, roundNumber);
                }
            }
        }
        private void DisplayRoundData(Round roundResult, int roundNumber)
        {
            boxResults.Text += $"Round {roundNumber}\r\n";
            boxResults.Text += $"Round Type: {roundResult.RoundType}\r\n";
            boxResults.Text += $"Qualified: {roundResult.Qualified}\r\n";
            boxResults.Text += $"Kudos Earned: {roundResult.Kudos}\r\n";
            boxResults.Text += $"Badge Earned: {roundResult.Badge}\r\n";
            boxResults.Text += $"\r\n";
        }
    }
}

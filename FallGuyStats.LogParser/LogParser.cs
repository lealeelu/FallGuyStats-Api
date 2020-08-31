using FallGuyStats.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace FallGuyStats.LogParser
{
    public class LogParser
    {
        Episode episode = new Episode();

        public static string previousTimestamp
            { get; set; } = "none";
        // read file
        // check if there is a new DTO detected via timestamp
        // parse DTO and store in DB
        // store new data in DB as an episode
        public static List<string> ReadLogData()
        {
            string currentUserAppData = System.Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string playerLogDataPath = currentUserAppData + "Low\\Mediatonic\\FallGuys_client\\Player.log";
            // string playerLogData = System.IO.File.ReadAllText(playerLogDataPath);
            List<string> playerLogData = System.IO.File.ReadLines(playerLogDataPath).ToList();
            return playerLogData;
        }

        public static string GetNewestEpisodeData(List<string> playerLogData)
        {
            List<string> episodeStartingPoints = playerLogData.FindAll(episodeDTO => episodeDTO.Contains("[CompletedEpisodeDto]"));
            string newestEpisodeDto = episodeStartingPoints.Last();
            string regexPattern = @"(\d+:\d+:\d+.\d+):";
            Regex timestampRegex = new Regex(regexPattern);
            Match episodeTimestamp = timestampRegex.Match(newestEpisodeDto);
            string newestEpisodeData = "";
            if (previousTimestamp != episodeTimestamp.Groups[1].Value) {
                previousTimestamp = episodeTimestamp.Groups[1].Value;
                int startIndex = playerLogData.IndexOf(newestEpisodeDto);
                int endIndex = playerLogData.FindLastIndex(badgeReport => badgeReport.StartsWith("> BadgeId:"));
                for (int i = startIndex; i <= endIndex; i++)
                {
                    newestEpisodeData += playerLogData[i];
                }
            }
            else
            {
                // Latest data is already returned; do nothing
            }
            newestEpisodeData += " 000000000000000000000000000000000";
            return newestEpisodeData;
        }

        public static Episode GetLastEpisodeStats(string lastEpisodeData)
        {
            Episode episodeResults = new Episode();

            int episodeKudosIndex = lastEpisodeData.IndexOf("Kudos:");
            string episodeKudosStateText = lastEpisodeData.Substring(episodeKudosIndex, 10);
            Regex episodeKudosRegex = new Regex(@"Kudos:.(\d+)");
            Match episodeKudosState = episodeKudosRegex.Match(episodeKudosStateText);

            int episodeFameIndex = lastEpisodeData.IndexOf("Fame:");
            string episodeFameStateText = lastEpisodeData.Substring(episodeFameIndex, 10);
            Regex episodeFameRegex = new Regex(@"Fame:.(\d+)");
            Match episodeFameState = episodeFameRegex.Match(episodeFameStateText);

            int episodeCrownsIndex = lastEpisodeData.IndexOf("Crowns:");
            string episodeCrownsStateText = lastEpisodeData.Substring(episodeCrownsIndex, 10);
            Regex episodeCrownsRegex = new Regex(@"Crowns:.(\d+)");
            Match episodeCrownsState = episodeCrownsRegex.Match(episodeCrownsStateText);

            int searchIndex = 0;
            int roundCount = 0;
            while ((searchIndex = lastEpisodeData.IndexOf("[Round", searchIndex)) != -1)
            {
                roundCount++;
                searchIndex++;
            }

            episodeResults.Kudos = Int32.Parse(episodeKudosState.Groups[1].Value);
            episodeResults.Fame = Int32.Parse(episodeFameState.Groups[1].Value);
            episodeResults.Crowns = Int32.Parse(episodeCrownsState.Groups[1].Value);
            episodeResults.RoundsPlayed = roundCount;
            return episodeResults;
        }

        public static Round GetRoundStats(string roundData, int roundNumber)
        {
            Round roundResults = new Round();

            // Get round type
            int roundIndex = roundData.IndexOf($"[Round {roundNumber}");
            string roundCodeStateText = roundData.Substring(roundIndex, 50);
            Regex roundCodeRegex = new Regex(@"\[Round \d \| (.+)\]");
            Match roundCodeState = roundCodeRegex.Match(roundCodeStateText);

            // Get qualification status for round
            int qualifiedIndex = roundData.IndexOf("Qualified:", roundIndex);
            string qualifiedStateText = roundData.Substring(qualifiedIndex, 25);
            Regex qualifiedRegex = new Regex(@"Qualified:.([a-zA-Z]+)");
            Match qualifiedState = qualifiedRegex.Match(qualifiedStateText);

            // Get starting position in round
            int positionIndex = roundData.IndexOf("Position:", roundIndex);
            string positionStateText = roundData.Substring(positionIndex, 25);
            Regex positionRegex = new Regex(@"Position:.(\d+)");
            Match positionState = positionRegex.Match(positionStateText);

            // Get Kudos earned in round
            int kudosIndex = roundData.IndexOf("Kudos:", roundIndex);
            string kudosStateText = roundData.Substring(kudosIndex, 25);
            Regex kudosRegex = new Regex(@"Kudos:.(\d+)");
            Match kudosState = kudosRegex.Match(kudosStateText);

            // Get Fame earned in round
            int fameIndex = roundData.IndexOf("Fame:", roundIndex);
            string fameStateText = roundData.Substring(fameIndex, 25);
            Regex fameRegex = new Regex(@"Fame:.(\d+)");
            Match fameState = fameRegex.Match(fameStateText);

            // Get round's bonus tier
            int bonusTierIndex = roundData.IndexOf("Bonus Tier:", roundIndex);
            string bonusTierStateText = roundData.Substring(bonusTierIndex, 25);
            Regex bonusTierRegex = new Regex(@"Bonus Tier:.(\d+)");
            Match bonusTierState = bonusTierRegex.Match(bonusTierStateText);

            int bonusTier;
            if (bonusTierState.Captures.Count == 0)
            {
                bonusTier = -1;
            }
            else
            {
                bonusTier = Int32.Parse(bonusTierState.Groups[1].Value);
            }


            // Get round's bonus Kudos
            int bonusKudosIndex = roundData.IndexOf("Bonus Kudos:", roundIndex);
            string bonusKudosStateText = roundData.Substring(bonusKudosIndex, 25);
            Regex bonusKudosRegex = new Regex(@"Bonus Kudos:.(\d+)");
            Match bonusKudosState = bonusKudosRegex.Match(bonusKudosStateText);

            // Get round's bonus Fame
            int bonusFameIndex = roundData.IndexOf("Bonus Fame:", roundIndex);
            string bonusFameStateText = roundData.Substring(bonusFameIndex, 16);
            Regex bonusFameRegex = new Regex(@"Bonus Fame:.(\d+)");
            Match bonusFameState = bonusFameRegex.Match(bonusFameStateText);

            // Get round's earned badge
            int badgeIndex = roundData.IndexOf("BadgeId:", roundIndex);
            string badgeStateText = roundData.Substring(badgeIndex, 16);
            Regex badgeRegex = new Regex(@"BadgeId:.([a-zA-Z]+)");
            Match badgeState = badgeRegex.Match(badgeStateText);
            string badgeId = "failure";
            if (bonusTierState.Captures.Count == 0 && qualifiedState.Groups[1].Value == "True")
            {
                badgeId = "Passed";
            }
            else if (qualifiedState.Groups[1].Value == "False")
            {
                badgeId = "Failure";
            }
            else
            {
                badgeId = badgeState.Groups[1].Value;
            }


            // Assemble round data object
            roundResults.RoundType = roundCodeState.Groups[1].Value;
            roundResults.Qualified = (qualifiedState.Groups[1].Value == "True");
            roundResults.Position = Int32.Parse(positionState.Groups[1].Value);
            roundResults.Kudos = Int32.Parse(kudosState.Groups[1].Value);
            roundResults.Fame = Int32.Parse(fameState.Groups[1].Value);
            roundResults.BonusTier = bonusTier;
            roundResults.BonusKudos = Int32.Parse(bonusKudosState.Groups[1].Value);
            roundResults.BonusFame = Int32.Parse(bonusFameState.Groups[1].Value);
            roundResults.Badge = badgeId;

            return roundResults;
        }
    }
}

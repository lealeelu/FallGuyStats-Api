// FallGuyStats Copyright (c) Leah Lee. All rights reserved.
// Licensed under the GNU General Public License v3.0
// Please Consider supporting the developer with some good good coffee: ko-fi.com/lealeelu

using FallGuyStats.Objects.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace FallGuyStats.Tools
{
    public static class LogParserV2
    {
        public const string epPattern = @"(?<finishedDate>\d+:\d+:\d+.\d+).+?Kudos: (?<episodeKudos>(\d)*).+?Fame: (?<episodeFame>(\d)*).+?Crowns: (?<crowns>(\d)*)";
        public const string roundPattern = @"\[Round (?<roundNumber>\d+) \| (?<roundName>\w+).+?Qualified: (?<qualified>\w+).+?Position: (?<position>\d*).+?Kudos: (?<kudos>\d*).+?Fame: (?<fame>\d*).+?Bonus Tier: (?<bonusTier>\d*).+?Bonus Kudos: (?<bonusKudos>\d*).+?Bonus Fame: (?<bonusFame>\d*).+?BadgeId: (?<badge>\w*)";
        public const string currentRoundPattern = @"\[StateGameLoading\] Finished loading game level, assumed to be (?<roundName>\w+)";

        public static string currentRound = "unknown";

        public static List<EpisodeEntity> GetEpisodesFromLog()
        {
            var currentUserAppData = System.Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var playerLogDataPath = currentUserAppData + "Low\\Mediatonic\\FallGuys_client\\Player.log";
            var episodeString = "";
            var currentGameString = "";

            try
            {
                if (!File.Exists(playerLogDataPath))
                {
                    Console.WriteLine($"Couldn't find Player.log file in {playerLogDataPath}");
                    return null;
                }
                using (FileStream fileStream = new FileStream(
                    playerLogDataPath,
                    FileMode.Open,
                    FileAccess.Read,
                    FileShare.ReadWrite))
                {
                    using (StreamReader streamReader = new StreamReader(fileStream))
                    {
                        while (!streamReader.EndOfStream)
                        {
                            var line = streamReader.ReadLine();
                            if (line.Contains("CompletedEpisodeDto") 
                                || line.StartsWith(">")
                                || line.StartsWith("[Round"))
                            {
                                episodeString += line;
                            }
                            if (line.Contains("[StateGameLoading] Fin"))
                            {
                                currentGameString = line;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error loading log file: " + ex.Message);
            }

            currentRound = GetCurrentRoundFromString(currentGameString);
            return GetEpisodesFromLogString(episodeString);
        }

        public static List<EpisodeEntity> GetEpisodesFromLogString(string logString)
        {
            var eps = new List<EpisodeEntity>();
            var matches = Regex.Matches(logString, epPattern, RegexOptions.Singleline);
            for (var i = 0; i < matches.Count; i++)
            {
                var epMatch = matches[i];
                if (epMatch.Success)
                {
                    var ep = new EpisodeEntity();
                    ep.Timestamp = epMatch.Groups["finishedDate"].Value ?? "";
                    ep.EpisodeFinished = ParseUtil.DateTimeParse(ep.Timestamp);
                    ep.Kudos = ParseUtil.IntParse(epMatch.Groups["episodeKudos"].Value);
                    ep.Crowns = ParseUtil.IntParse(epMatch.Groups["crowns"].Value);
                    ep.Fame = ParseUtil.IntParse(epMatch.Groups["episodeFame"].Value);
                    string roundString;
                    if (i + 1 < matches.Count)
                        roundString = logString.Substring(epMatch.Index, matches[i + 1].Index - epMatch.Index);
                    else roundString = logString.Substring(epMatch.Index);
                    ep.RoundEntities = GetRoundsFromString(roundString);
                    eps.Add(ep);
                }
            }

            return eps;
        }

        private static List<RoundEntity> GetRoundsFromString(string roundString)
        {
            var roundMatches = Regex.Matches(roundString, roundPattern, RegexOptions.Singleline);
            var results = new List<RoundEntity>();

            foreach (Match roundMatch in roundMatches)
            {
                var round = new RoundEntity();
                if (roundMatch.Success)
                {
                    round.RoundType = roundMatch.Groups["roundName"].Value ?? string.Empty;
                    round.Qualified = ParseUtil.BoolParse(roundMatch.Groups["qualified"].Value);
                    round.Position = ParseUtil.IntParse(roundMatch.Groups["position"].Value);
                    round.Kudos = ParseUtil.IntParse(roundMatch.Groups["kudos"].Value);
                    round.Fame = ParseUtil.IntParse(roundMatch.Groups["fame"].Value);
                    round.BonusTier = ParseUtil.IntParse(roundMatch.Groups["bonusTier"].Value);
                    round.BonusKudos = ParseUtil.IntParse(roundMatch.Groups["bonusKudos"].Value);
                    round.BonusFame = ParseUtil.IntParse(roundMatch.Groups["bonusFame"].Value);
                    round.Badge = roundMatch.Groups["badge"].Value ?? string.Empty;
                    results.Add(round);
                }
            }
            return results;
        }

        private static string GetCurrentRoundFromString(string roundString)
        {
            var roundMatches = Regex.Matches(roundString, currentRoundPattern, RegexOptions.Singleline);

            var currentRound = "Unknown";
            if (roundMatches.Count > 0)
            {
                currentRound = roundMatches.Last().Groups["roundName"].Value ?? string.Empty;
            }
            return currentRound;
        }
    }
}

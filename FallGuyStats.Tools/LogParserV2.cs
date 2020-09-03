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
        public const string currentRoundPattern = @"\[StateGameLoading\] Finished loading game level, assumed to be '(?<roundName>\w+)'";

        public static List<EpisodeEntity> GetEpisodesFromLog()
        {
            var currentUserAppData = System.Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var playerLogDataPath = currentUserAppData + "Low\\Mediatonic\\FallGuys_client\\Player.log";
            var EpisodeList = new List<EpisodeEntity>();
            var episodeString = "";

            try
            {
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
                            if (line.Contains("CompletedEpisodeDto") || line.StartsWith(">") || line.StartsWith("[Round"))
                            {
                                episodeString += line;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error loading log file: " + ex.Message);
            }

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
                    ep.EpisodeFinished = Util.DateTimeParse(ep.Timestamp);
                    ep.Kudos = Util.IntParse(epMatch.Groups["episodeKudos"].Value);
                    ep.Crowns = Util.IntParse(epMatch.Groups["crowns"].Value);
                    ep.Fame = Util.IntParse(epMatch.Groups["episodeFame"].Value);
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
                    round.Qualified = Util.BoolParse(roundMatch.Groups["qualified"].Value);
                    round.Position = Util.IntParse(roundMatch.Groups["position"].Value);
                    round.Kudos = Util.IntParse(roundMatch.Groups["kudos"].Value);
                    round.Fame = Util.IntParse(roundMatch.Groups["fame"].Value);
                    round.BonusTier = Util.IntParse(roundMatch.Groups["bonusTier"].Value);
                    round.BonusKudos = Util.IntParse(roundMatch.Groups["bonusKudos"].Value);
                    round.BonusFame = Util.IntParse(roundMatch.Groups["bonusFame"].Value);
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
            if (roundMatches.Last().Success)
            {
                currentRound = roundMatches.Last().Groups["roundName"].Value ?? string.Empty;
            }
            return currentRound;
        }
    }
}

using System.ComponentModel.DataAnnotations;
using System.Linq;
using cod_data_analyzer.Migrations;
using cod_data_analyzer.Models;
using cod_data_analyzer.Services.Util;
using cod_data_analyzer.ViewModels;
using Microsoft.EntityFrameworkCore;
namespace cod_data_analyzer.Services.Helper
{
    public class AnalyzerReportHelper
    {
        public List<KeyValuePair<string, bool>> GetSettingsEnabled(AnalyzerDataViewModel viewModel)
            => [
                new KeyValuePair<string, bool>("MatchQuitThreshold", viewModel.MatchQuitThresholdEnabled),
                new KeyValuePair<string, bool>("MinTimePlayedTopGame", viewModel.MinTimePlayedTopGameEnabled),
                new KeyValuePair<string, bool>("MinTimePlayedTopMap", viewModel.MinTimePlayedTopMapEnabled),
                new KeyValuePair<string, bool>("MinScore", viewModel.MinScoreEnabled),
                new KeyValuePair<string, bool>("MinSkill", viewModel.MinSkillEnabled),
                new KeyValuePair<string, bool>("DateRange", viewModel.DateRangeEnabled),
            ];
        public ReportAverages GetReportAverages(ReportTotals totals, int matchCount)
        {
            float kdRatio = MathUtil.Average(totals.TotalKills, totals.TotalDeaths);
            float winLossRatio = MathUtil.Average(totals.TotalWins, totals.TotalLosses);
            float killAssistRatio = MathUtil.Average(totals.TotalAssists, totals.TotalKills);
            float edRatio = MathUtil.Average(totals.TotalEliminations, totals.TotalDeaths);

            float eliminationAverage = MathUtil.Average(totals.TotalEliminations, matchCount);
            float killAverage = MathUtil.Average(totals.TotalKills, matchCount);
            float headshotAverage = MathUtil.Average(totals.TotalHeadshots, matchCount);
            float assistAverage = MathUtil.Average(totals.TotalAssists, matchCount);
            float deathAverage = MathUtil.Average(totals.TotalDeaths, matchCount);
            float averageSr = MathUtil.Average(totals.TotalSkill, matchCount);

            return new ReportAverages
            {
                KdRatio = kdRatio,
                WinLossRatio = winLossRatio,
                KillAssistRatio = killAssistRatio,
                KillAverage = killAverage,
                EliminationAverage = eliminationAverage,
                HeadshotAverage = headshotAverage,
                AssistAverage = assistAverage,
                DeathAverage = deathAverage,
                AverageSr = averageSr,
                EdRatio = edRatio
            };
        }

        public ReportTotals GetReportTotals(List<Match> matches)
        {
            int totalKills
                = matches.Sum(am => am.PlayerStats.Kills);
            int totalAssists
                = matches.Sum(am => am.PlayerStats.Assists);
            int totalDeaths = matches.Sum(am => am.PlayerStats.Deaths);
            int totalHits
                = matches.Sum(am => am.PlayerStats.Hits);
            int totalMisses
                = matches.Sum(am => am.PlayerStats.Shots - am.PlayerStats.Hits);
            int totalSkill
                = matches.Sum(am => am.PlayerStats.Skill);
            int totalHeadshots
                = matches.Sum(am => am.PlayerStats.Headshots);

            int totalWins
                = matches.Count(am => am.MatchWin);
            int totalLosses
                = matches.Count(am => !am.MatchWin);

            int totalEliminations = totalKills + totalAssists;

            return new ReportTotals
            {
                TotalKills = totalKills,
                TotalEliminations = totalEliminations,
                TotalAssists = totalAssists,
                TotalDeaths = totalDeaths,
                TotalHits = totalHits,
                TotalMisses = totalMisses,
                TotalWins = totalWins,
                TotalLosses = totalLosses,
                TotalHeadshots = totalHeadshots,
                TotalSkill = totalSkill
            };
        }
    }

    public class AnalalyzerReportValidator
    {
        public AnalyzerSettings Settings { get; set; } = new AnalyzerSettings();
        public bool MatchQuitEnabled { get; set; }
        public bool MinScoreEnabled { get; set; }
        public bool MinSkillEnabled { get; set; }
        public bool DateRangeEnabled { get; set; }
        public bool IsModeWhitelist {  get; set; }
        public bool IsMapWhitelist { get; set; }
        public int MatchQuitThreshold { get; set; }
        public int MinScore { get; set; }
        public int MinSkill { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<int> ModeIds { get; set; }
        public List<int> MapIds { get; set; }
        public AnalalyzerReportValidator(
            AnalyzerSettings settings, List<KeyValuePair<string, bool>> settingEnabled)
        {
            MatchQuitEnabled = settingEnabled.Find(se => se.Key == "MatchQuitThreshold").Value;
            MinScoreEnabled = settingEnabled.Find(se => se.Key == "MinScore").Value;
            MinSkillEnabled = settingEnabled.Find(se => se.Key == "MinSkill").Value;
            DateRangeEnabled = settingEnabled.Find(se => se.Key == "DateRange").Value;

            IsMapWhitelist = settings.isMapWhitelist;
            IsModeWhitelist = settings.isGameModeWhitelist;
            MatchQuitThreshold = settings.MatchQuitThreshold;

            IsMapWhitelist = settings.isMapWhitelist;
            IsModeWhitelist = settings.isGameModeWhitelist;
            MatchQuitThreshold = settings.MatchQuitThreshold;
            MinScore = settings.MinScore;
            MinSkill = settings.MinSkill;
            ModeIds = settings.SelectedGameModes
                                .Select(m => m.GameModeId)
                                .ToList();
            MapIds = settings.SelectedMaps
                                .Select(m => m.MapId)
                                .ToList();


            StartDate = settings.StartDate;
            EndDate = settings.EndDate;
        }

        public List<Match> FilterMatches(List<Match> matches)
        {
            List<Match> validMatches = [];
            foreach (Match match in matches)
            {
                if (ValidMatch(match))
                    validMatches.Add(match);
            }
            return validMatches;
        }
        public bool ValidMatch(Match match)
        {
            int matchLength = match.MatchLength;
            if (MatchQuitThreshold > matchLength && MatchQuitEnabled)
                return false;

            int minScore = match.PlayerStats.Score;
            if (MinScore >  minScore && MinScoreEnabled)
                return false;

            int minSkill = match.PlayerStats.Skill;
            if (MinSkill > minSkill && MinSkillEnabled)
                return false;

            DateTime matchStart = match.MatchStart;
            if (!ValidDateRange(matchStart, StartDate, EndDate))
                return false;

            int matchGameModeId = match.GameModeId;
            if (!ModeIds.Contains(matchGameModeId))
                return false;

            int matchMap = match.MapId;
            if (!MapIds.Contains(matchMap))
                return false;


            return true;
        }

        public bool ValidDateRange(DateTime matchStart,
            DateTime rangeStart, DateTime rangeEnd)
                => matchStart > rangeStart && matchStart < rangeEnd;
    }
}

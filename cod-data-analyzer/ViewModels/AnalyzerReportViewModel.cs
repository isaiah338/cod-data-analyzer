using cod_data_analyzer.Models;
namespace cod_data_analyzer.ViewModels
{
    public class AnalyzerReportViewModel
    {
        public List<Match> Matches;
        public int MatchCount { get; set; }
        public ReportTotals ReportTotals { get; set; }
        public ReportAverages ReportAverages { get; set; }
        public AnalyzerSettings AnalyzerSettings { get; set; }
        public List<KeyValuePair<string, bool>>? IsSettingEnabled { get; set; }

    }
    public class ReportTotals
    {
        public int TotalKills { get; set; }
        public int TotalEliminations { get; set; }
        public int TotalAssists { get; set; }
        public int TotalDeaths { get; set; }
        public int TotalHits { get; set; }
        public int TotalMisses { get; set; }
        public int TotalWins { get; set; }
        public int TotalLosses { get; set; }
        public int TotalHeadshots { get; set; }
        public int TotalSkill {  get; set; }
    }
    public class ReportAverages
    {
        public float KdRatio { get; set; }
        public float WinLossRatio { get; set; }
        public float KillAssistRatio { get; set; }
        public float EdRatio { get; set; }


        public float EliminationAverage { get; set; }
        public float KillAverage { get; set; }
        public float HeadshotAverage { get; set; }
        public float AssistAverage { get; set; }
        public float DeathAverage { get; set; }
        public float AverageDamageTaken { get; set; }
        public float AverageDamageDealt { get; set; }
        public float AverageSr { get; set; }
    }

}

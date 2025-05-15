using cod_data_analyzer.Models;
namespace cod_data_analyzer.ViewModels
{
    public class AnalyzerDataViewModel
    {
        public List<Match> Matches { get; set; }
        public AnalyzerSettings Settings { get; set; }
        public List<Map> Maps { get; set; }
        public List<GameMode> CoreGameModes { get; set; }
        public List<GameMode> PartyGameModes { get; set; }
        public bool MatchQuitThresholdEnabled { get; set; }  = false;
        public bool MinTimePlayedTopGameEnabled { get; set; } = false;
        public bool MinTimePlayedTopMapEnabled { get; set; } = false;
        public bool MinScoreEnabled { get; set; } = false;
        public bool MinSkillEnabled { get; set; } = false;
        public bool DateRangeEnabled { get; set; } = false;
        public bool IsLoad { get; set; } = false;
    }


}

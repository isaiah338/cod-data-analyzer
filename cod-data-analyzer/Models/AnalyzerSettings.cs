namespace cod_data_analyzer.Models
{
    public class AnalyzerSettings
    {
        public int AnalyzerSettingsId { get; set; }
        public int MatchQuitThreshold { get; set; } = 0;
        public int MinTimePlayedTopGame { get; set; } = 15;
        public int MinTimePlayedTopMap { get; set;}   = 15;
        public int MinScore { get; set; }     = 0;
        
        public int MinSkill { get; set; } = 0;
        public bool isMapWhitelist { get; set; } = true;
        public int SrGraphFrequency { get; set; } = 5;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<Map> SelectedMaps { get; set; } = [];
        //public AnalyzerSettingsMaps AnalyzerSettingsMaps { get; set; }
        public bool isGameModeWhitelist { get; set; } = true;
        public List<GameMode> SelectedGameModes { get; set; } = [];

        //public ICollection<AnalyzerSettingsMode> AnalyzerSettingsModes { get; set; } = new List<AnalyzerSettingsMode>();
        //public ICollection<AnalyzerSettingsMap> AnalyzerSettingsMaps { get; set; } = new List<AnalyzerSettingsMap>();
    }

    public class AnalyzerSettingsMode
    {
        public int AnalyzerSettingsModeId { get; set; }

        public int AnalyzerSettingsId { get; set; }
        public AnalyzerSettings? AnalyzerSettings { get; set; }

        public int GameModeId { get; set; }
        public GameMode? GameMode { get; set; }
        //public ICollection<GameMode> AnalyzerSettingsModes { get; set; } = new List<GameMode>();
    }
    public class AnalyzerSettingsMap
    {
        public int AnalyzerSettingsMapId { get; set; }

        public int AnalyzerSettingsId { get; set; }
        public AnalyzerSettings? AnalyzerSettings { get; set; }

        public int MapId { get; set; }
        public Map? Map { get; set; }
        //public ICollection<Map> AnalyzerSettingsModes { get; set; } = new List<Map>();
    }
}

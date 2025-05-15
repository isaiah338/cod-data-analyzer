namespace cod_data_analyzer.Models
{
    public class UserAnalyzerSettings
    {
        public int UserAnalyzerSettingsId { get; set; }
        public int AnalyzerSettingsId { get; set; }
        public AnalyzerSettings? AnalyzerSettings { get; set; }
    }
}

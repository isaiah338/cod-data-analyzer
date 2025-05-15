using System.ComponentModel.DataAnnotations;

namespace cod_data_analyzer.Models
{
    public class Map
    {
        [Key]
        public int MapId { get; set; }
        [Required]
        public string MapName { get; set; }
        [Required]
        public bool IsFaceoff { get; set; }
        [Required]
        public bool IsSmall { get; set; }
        [Required]
        public bool IsSpecialty { get; set; }
        //public ICollection<AnalyzerSettingsMap> AnalyzerSettingsMaps { get; set; }

    }
}

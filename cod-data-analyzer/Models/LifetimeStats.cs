using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cod_data_analyzer.Models
{
    public class LifetimeStats

    {
        [Key]
        public int LifetimeStatsId { get; set; }
        [Required]
        public int LifetimeWins { get; set; }
        [Required]
        public int LifetimeLosses { get; set; }
        [Required]
        public int LifetimeKills { get; set; }
        [Required]
        public int LifetimeDeaths { get; set; }
        [Required]
        public int LifetimeHits { get; set; }
        [Required]
        public int LifetimeMisses { get; set; }
        [ForeignKey("MatchId")]
        public int MatchId { get; set; }
        public Match? Match { get; set; }
    }
}

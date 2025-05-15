using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cod_data_analyzer.Models
{
    public class PlayerStats
    {
        [Key]
        public int PlayerStatsId { get; set; }
        [Required]
        public int Skill {  get; set; } 
        [Required]
        public int Score { get; set; }
        [Required]
        public int Shots { get; set; }
        [Required]
        public int LongestStreak { get; set; }
        [Required]
        public int Headshots { get; set; }
        [Required]
        public int Executions { get; set; }
        [Required]
        public int Suicides { get; set; }
        [Required]
        public double PercentTimeMoving { get; set; }
        [Required]
        public int Kills { get; set; }
        [Required]
        public int Deaths { get; set; }
        [Required]
        public int DamageDone { get; set; }
        [Required]
        public int Hits { get; set; }
        [Required]
        public int Assists { get; set; }
        [ForeignKey("MatchId")]
        public int MatchId { get; set; }
        public Match? Match { get; set; }
    }
}

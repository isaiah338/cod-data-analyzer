using System.ComponentModel.DataAnnotations;

namespace cod_data_analyzer.Models
{
    public class Match
    {
        [Key]
        public int MatchId { get; set; }
        [Required]
        public DateTime MatchStart { get; set; }
        [Required]
        public DateTime MatchEnd { get; set; }
        [Required]
        public int MatchLength { get; set; }
        [Required]
        public bool MatchWin { get; set; }

        // one-to-one:
        // https://learn.microsoft.com/en-us/ef/core/modeling/relationships/one-to-one
        public LifetimeStats? LifetimeStats { get; set; }
        public Map? Map { get; set; }
        public int MapId { get; set; }
        public PlayerStats? PlayerStats { get; set; }
        public XpStats? XpStats { get; set; }
        public int GameModeId { get; set; }
        public GameMode? GameMode { get; set; }
        public ApplicationUser? User { get; set; }
        public string UserId { get; set; }
    }
}

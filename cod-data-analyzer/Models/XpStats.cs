using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cod_data_analyzer.Models
{
    public class XpStats
    {
        [Key]
        public int XpStatsId { get; set; }
        [Required]
        public int TotalXp {  get; set; }
        [Required]
        public int ChallengeXp { get; set; }
        [Required]
        public int ScoreXp { get; set; }
        [Required]
        public int MatchXp { get; set; }
        [Required]
        public int MedalXp { get; set; }
        [Required]
        public int MiscXp { get; set; }
        [Required]
        public int AccoladeXp { get; set; }
        [Required]
        public int WeaponXp { get; set; }
        [Required]
        public int OperatorXp { get; set; }
        [Required]
        public int BattlepassXp { get; set; }
        [Required]
        public int RankStart {  get; set; }
        [Required]
        public int RankEnd { get; set; }
        [ForeignKey("MatchId")]
        public int MatchId { get; set; }
        public Match? Match { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace cod_data_analyzer.Models
{
    public class GameMode
    {
        [Key]
        public int GameModeId { get; set; }
        [AllowNull]
        public string? GameModeDescription { get; set; }
        
        [Required, NotNull]
        [ForeignKey("GameType")]
        public int GameTypeId { get; set; }
        public GameType? GameType { get; set; } // core, hardcore, party, third-person

        [Required, NotNull]
        [ForeignKey("GameTitle")]
        public int GameTitleId { get; set; }
        public GameTitle? GameTitle { get; set; } // team deathmatch, free for all, search and destroy, etc...

    }
    public class GameType
    {
        [Key]
        public int GameTypeId { get; set; }
        [Required, NotNull]
        public string GameTypeName { get; set; }
        public ICollection<GameMode>? GameModes { get; set; } // navigation object
    }
    public class GameTitle
    {
        [Key]
        public int GameTitleId { get; set; }
        [AllowNull]
        public string GameTitleCode { get; set; } // free for all = ffa, team deathmatch = tdm, search and destroy = s&d, etc...
        [Required, NotNull]
        public string GameTitleName { get; set; }
        public ICollection<GameMode>? GameModes {  get; set; } // navigation object
    }
}

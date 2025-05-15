using cod_data_analyzer.Models;

namespace cod_data_analyzer.ViewModels
{
    public class AdminModesViewModel
    {
        public List<GameMode> GameModes { get; set; }
        public List<GameType> GameTypes { get; set; }
        public List<GameTitle> GameTitles { get; set; }
        public GameType EditType { get; set; }
        public GameTitle EditTitle { get; set; }
        public GameMode EditGameMode { get; set; }
    }
}

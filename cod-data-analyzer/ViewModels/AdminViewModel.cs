using cod_data_analyzer.Models;
namespace cod_data_analyzer.ViewModels
{
    public class AdminViewModel
    {
        public List<Map> Maps { get; set; }
        public List<Match> Matches { get; set; }
        public List<GameTitle> Modes { get; set; }
    }
}

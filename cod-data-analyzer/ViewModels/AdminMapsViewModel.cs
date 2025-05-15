using cod_data_analyzer.Models;

namespace cod_data_analyzer.ViewModels
{
    public class AdminMapsViewModel
    {
        public Map EditMap {  get; set; }
        public List<Map> AllMaps { get; set; }
        public string SuccessMessage { get; set; } = "";
        public string ErrorMessage { get; set; } = "";
    }
}

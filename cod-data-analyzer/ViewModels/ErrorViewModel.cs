using cod_data_analyzer.Models;

namespace cod_data_analyzer.ViewModels
{
    public class ErrorViewModel
    {
        // 0-title,1=description,3=severity
        /// severity: 0-none, 1-warning, 2-critical <summary>
        /// severity: 0-none, 1-warning, 2-critical
        /// </summary>
        /// 

        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
        public List<UserError> Errors { get; set; }
        public string ReturnUrl { get; set; } = "/Index";
        public string ReturnAction { get; set; } = "Index";
        public string ReturnController { get; set; } = "Home";
    }
}

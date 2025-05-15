namespace cod_data_analyzer.Models
{
    public class UserError
    {
        public DateTime? Timestamp { get; set; }
        public string Header { get; set; }
        public string Detail { get; set; } = "";
        public int Severity { get; set; } = 0;
    }
}

using System.ComponentModel.DataAnnotations;

namespace cod_data_analyzer.ViewModels
{
    public class CreateUserViewModel
    {
        [Required]
        public String UserName  { get; set; }
        [Required]
        public String Password { get; set; }
    }
}

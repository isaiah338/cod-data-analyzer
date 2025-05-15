using System.ComponentModel.DataAnnotations;

namespace cod_data_analyzer.Models
{
    public class LoginViewModel
    {
        [Required]
        // The UIHints indicate to the View files how to better display those properties
        [UIHint("username")]
        public string UserName { get; set; }

        [Required]
        [UIHint("password")]
        public string Password { get; set; }
    }
}

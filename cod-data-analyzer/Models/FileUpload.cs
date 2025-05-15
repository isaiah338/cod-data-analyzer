using System.ComponentModel.DataAnnotations;

namespace cod_data_analyzer.Models
{
    public class FileUpload
    {
        [Required(ErrorMessage = "Please select a file.")]
        public IFormFile File;
        public DateTime UploadDateTime;
    }
}

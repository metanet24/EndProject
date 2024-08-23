using System.ComponentModel.DataAnnotations;

namespace MBEAUTY.ViewModels.AboutVms
{
    public class AboutAddVM
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public IFormFile Photo { get; set; }
    }
}

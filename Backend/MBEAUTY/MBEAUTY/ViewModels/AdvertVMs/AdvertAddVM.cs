using System.ComponentModel.DataAnnotations;

namespace MBEAUTY.ViewModels.AdvertVMs
{
    public class AdvertAddVM
    {
        [Required]
        public IFormFile Photo { get; set; }
    }
}

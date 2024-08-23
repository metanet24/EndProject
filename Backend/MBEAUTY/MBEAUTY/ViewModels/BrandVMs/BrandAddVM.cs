using System.ComponentModel.DataAnnotations;

namespace MBEAUTY.ViewModels.BrandVMs
{
    public class BrandAddVM
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public IFormFile Photo { get; set; }
    }
}

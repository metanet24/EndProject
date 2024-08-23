using System.ComponentModel.DataAnnotations;

namespace MBEAUTY.ViewModels.FamousVms
{
    public class FamousAddVM
    {
        [Required]
        public string FullName { get; set; }

        [Required]
        public string About { get; set; }

        [Required]
        public IFormFile Photo { get; set; }
    }
}

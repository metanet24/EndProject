using System.ComponentModel.DataAnnotations;

namespace MBEAUTY.ViewModels.FamousVms
{
    public class FamousEditVM
    {
        public int Id { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        public string About { get; set; }

        public string Image { get; set; }
        public IFormFile Photo { get; set; }
    }
}

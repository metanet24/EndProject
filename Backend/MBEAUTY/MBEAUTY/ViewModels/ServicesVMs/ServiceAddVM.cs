using System.ComponentModel.DataAnnotations;

namespace MBEAUTY.ViewModels.ServicesVMs
{
    public class ServiceAddVM
    {
        [Required]
        public string Icon { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }
    }
}

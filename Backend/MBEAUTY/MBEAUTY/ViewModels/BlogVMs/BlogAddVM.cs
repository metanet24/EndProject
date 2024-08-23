using System.ComponentModel.DataAnnotations;

namespace MBEAUTY.ViewModels.BlogVMs
{
    public class BlogAddVM
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public IFormFile[] Photos { get; set; }
    }
}

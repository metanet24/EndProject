using MBEAUTY.Models;
using System.ComponentModel.DataAnnotations;

namespace MBEAUTY.ViewModels.BlogVMs
{
    public class BlogEditVM
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public IFormFile[] Photos { get; set; }
        public IEnumerable<BlogImage> BlogImages { get; set; }
    }
}

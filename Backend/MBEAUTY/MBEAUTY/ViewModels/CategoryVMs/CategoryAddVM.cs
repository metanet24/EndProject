using System.ComponentModel.DataAnnotations;

namespace MBEAUTY.ViewModels.CategoryVMs
{
    public class CategoryAddVM
    {
        [Required]
        public string Name { get; set; }
    }
}

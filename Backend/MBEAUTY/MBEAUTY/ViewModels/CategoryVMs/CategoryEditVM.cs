using System.ComponentModel.DataAnnotations;

namespace MBEAUTY.ViewModels.CategoryVMs
{
    public class CategoryEditVM
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}

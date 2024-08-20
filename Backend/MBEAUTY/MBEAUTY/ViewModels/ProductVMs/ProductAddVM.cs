using System.ComponentModel.DataAnnotations;

namespace MBEAUTY.ViewModels.ProductVMs
{
    public class ProductAddVM
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int DiscountPercent { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string SkinType { get; set; }

        [Required]
        public string Shades { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        public int BrandId { get; set; }

        [Required]
        public IFormFile[] Photos { get; set; }
    }
}

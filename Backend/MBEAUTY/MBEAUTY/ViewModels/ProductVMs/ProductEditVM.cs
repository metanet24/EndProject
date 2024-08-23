using MBEAUTY.Models;
using System.ComponentModel.DataAnnotations;

namespace MBEAUTY.ViewModels.ProductVMs
{
    public class ProductEditVM
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        public int BrandId { get; set; }

        public string BrandName { get; set; }

        [Required]
        public string SkinType { get; set; }

        [Required]
        public string Shades { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int DiscountPercent { get; set; }

        public IFormFile[] Photos { get; set; }

        public ICollection<ProductImage> ProductImages { get; set; }
    }
}

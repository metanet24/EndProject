using Microsoft.EntityFrameworkCore;

namespace MBEAUTY.Models
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        [Precision(18, 6)]
        public decimal Price { get; set; }
        public int DiscountPercent { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int BrandId { get; set; }
        public Brand Brand { get; set; }
        public AdditionalInfo AdditionalInfo { get; set; }
        public ICollection<ProductImage> ProductImages { get; set; }
        public ICollection<BasketProduct> BasketProducts { get; set; }
    }
}

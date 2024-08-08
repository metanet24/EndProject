using MBEAUTY.Models;

namespace MBEAUTY.ViewModels.ProductVMs
{
    public class ProductListVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int DiscountPercent { get; set; }
        public string BrandName { get; set; }
        public string CategoryName { get; set; }
        public IEnumerable<ProductImage> ProductImages { get; set; }
    }
}

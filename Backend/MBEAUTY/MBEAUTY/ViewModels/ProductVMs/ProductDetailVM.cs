using MBEAUTY.Models;

namespace MBEAUTY.ViewModels.ProductVMs
{
    public class ProductDetailVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int BrandId { get; set; }
        public string BrandName { get; set; }
        public string SkinType { get; set; }
        public string Shades { get; set; }
        public decimal Price { get; set; }
        public int DiscountPercent { get; set; }
        public IEnumerable<ProductImage> ProductImages { get; set; }
        public IEnumerable<ProductListVM> Products { get; set; }
    }
}

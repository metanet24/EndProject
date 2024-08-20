using MBEAUTY.Helpers;
using MBEAUTY.ViewModels.AdvertVMs;
using MBEAUTY.ViewModels.BrandVMs;
using MBEAUTY.ViewModels.CategoryVMs;
using MBEAUTY.ViewModels.ProductVMs;

namespace MBEAUTY.ViewModels
{
    public class ShopVM
    {
        public Paginate<ProductListVM> PaginateProducts { get; set; }
        public IEnumerable<ProductListVM> Products { get; set; }
        public IEnumerable<CategoryListVM> Categories { get; set; }
        public IEnumerable<BrandListVM> Brands { get; set; }
        public AdvertVM Advert { get; set; }
        public ProductDetailVM Product { get; set; }

        public int Page { get; set; } = 1;
        public int Take { get; set; } = 6;
        public string SearchText { get; set; }
        public int? CategoryId { get; set; }
        public int? BrandId { get; set; }
    }
}

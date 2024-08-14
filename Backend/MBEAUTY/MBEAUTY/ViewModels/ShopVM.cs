using MBEAUTY.ViewModels.AdvertVMs;
using MBEAUTY.ViewModels.BrandVMs;
using MBEAUTY.ViewModels.CategoryVMs;
using MBEAUTY.ViewModels.ProductVMs;

namespace MBEAUTY.ViewModels
{
    public class ShopVM
    {
        public IEnumerable<ProductListVM> Products { get; set; }
        public IEnumerable<CategoryListVM> Categories { get; set; }
        public IEnumerable<BrandListVM> Brands { get; set; }
        public AdvertVM Advert { get; set; }
    }
}

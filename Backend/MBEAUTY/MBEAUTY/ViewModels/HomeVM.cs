using MBEAUTY.ViewModels.BannnerVMs;
using MBEAUTY.ViewModels.BrandVMs;
using MBEAUTY.ViewModels.ProductVMs;
using MBEAUTY.ViewModels.SliderVMs;

namespace MBEAUTY.ViewModels
{
    public class HomeVM
    {
        public IEnumerable<SliderListVM> Sliders { get; set; }
        public IEnumerable<ProductListVM> Products { get; set; }
        public IEnumerable<BrandListVM> Brands { get; set; }
        public BannerVM Banner { get; set; }
    }
}

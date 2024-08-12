using MBEAUTY.ViewModels.BannnerVMs;
using MBEAUTY.ViewModels.BlogVMs;
using MBEAUTY.ViewModels.BrandVMs;
using MBEAUTY.ViewModels.ProductVMs;
using MBEAUTY.ViewModels.ServicesVMs;
using MBEAUTY.ViewModels.SliderVMs;

namespace MBEAUTY.ViewModels
{
    public class HomeVM
    {
        public IEnumerable<SliderListVM> Sliders { get; set; }
        public IEnumerable<ProductListVM> Products { get; set; }
        public IEnumerable<BrandListVM> Brands { get; set; }
        public IEnumerable<ServiceListVM> Services { get; set; }
        public IEnumerable<BlogListVM> Blogs { get; set; }
        public BannerVM Banner { get; set; }
    }
}

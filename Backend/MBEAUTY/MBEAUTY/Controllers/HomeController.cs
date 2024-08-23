using AutoMapper;
using MBEAUTY.Services.Interfaces;
using MBEAUTY.ViewModels;
using MBEAUTY.ViewModels.BlogVMs;
using MBEAUTY.ViewModels.BrandVMs;
using MBEAUTY.ViewModels.ProductVMs;
using Microsoft.AspNetCore.Mvc;

namespace MBEAUTY.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISliderService _sliderService;
        private readonly IProductService _productService;
        private readonly IBannerService _bannerService;
        private readonly IBrandService _brandService;
        private readonly IServiceService _serviceService;
        private readonly IBlogService _blogService;
        private readonly IMapper _mapper;

        public HomeController(ISliderService sliderService, IProductService productService, IBannerService bannerService, IBrandService brandService, IServiceService serviceService, IBlogService blogService, IMapper mapper)
        {
            _sliderService = sliderService;
            _productService = productService;
            _bannerService = bannerService;
            _brandService = brandService;
            _serviceService = serviceService;
            _blogService = blogService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            HomeVM model = new()
            {
                Sliders = await _sliderService.GetAllAsync(),
                Products = _mapper.Map<IEnumerable<ProductListVM>>(await _productService.GetAllAsync()),
                Banner = await _bannerService.GetAsync(),
                Brands = _mapper.Map<IEnumerable<BrandListVM>>(await _brandService.GetAllAsync()),
                Services = await _serviceService.GetAllAsync(),
                Blogs = _mapper.Map<IEnumerable<BlogListVM>>(await _blogService.GetAllAsync())
            };

            return View(model);
        }
    }
}

using MBEAUTY.Services.Interfaces;
using MBEAUTY.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace MBEAUTY.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISliderService _sliderService;
        private readonly IProductService _productService;
        private readonly IBannerService _bannerService;
        private readonly IBrandService _brandService;

        public HomeController(ISliderService sliderService, IProductService productService, IBannerService bannerService, IBrandService brandService)
        {
            _sliderService = sliderService;
            _productService = productService;
            _bannerService = bannerService;
            _brandService = brandService;
        }

        public async Task<IActionResult> Index()
        {
            HomeVM model = new()
            {
                Sliders = await _sliderService.GetAllAsync(),
                Products = await _productService.GetAllAsync(),
                Banner = await _bannerService.GetAsync(),
                Brands = await _brandService.GetAllAsync()
            };

            return View(model);
        }
    }
}

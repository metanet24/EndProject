using MBEAUTY.Services.Interfaces;
using MBEAUTY.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace MBEAUTY.Controllers
{
    public class ShopController : Controller
    {
        private readonly IProductService _productService;
        private readonly IBrandService _brandService;
        private readonly ICategoryService _categoryService;
        private readonly IAdvertService _advertService;

        public ShopController(IProductService productService, IBrandService brandService, ICategoryService categoryService, IAdvertService advertService)
        {
            _productService = productService;
            _brandService = brandService;
            _categoryService = categoryService;
            _advertService = advertService;
        }

        public async Task<IActionResult> Index()
        {
            ShopVM model = new()
            {
                Products = await _productService.GetAllAsync(),
                Categories = await _categoryService.GetAllAsync(),
                Brands = await _brandService.GetAllAsync(),
                Advert = await _advertService.GetAsync()
            };

            return View(model);
        }
    }
}

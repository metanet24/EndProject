using Fruitables_Backend.Helpers;
using MBEAUTY.Helpers;
using MBEAUTY.Services.Interfaces;
using MBEAUTY.ViewModels.AdditionalInfoVMs;
using MBEAUTY.ViewModels.ProductImageVMs;
using MBEAUTY.ViewModels.ProductVMs;
using Microsoft.AspNetCore.Mvc;

namespace MBEAUTY.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IProductImageService _productImageService;
        private readonly ICategoryService _categoryService;
        private readonly IBrandService _brandService;
        private readonly IAdditionalInfoService _additionalInfoService;
        private readonly IWebHostEnvironment _environment;

        public ProductController(IProductService productService, ICategoryService categoryService, IWebHostEnvironment environment, IBrandService brandService, IAdditionalInfoService additionalInfoService, IProductImageService productImageService)
        {
            _productService = productService;
            _categoryService = categoryService;
            _environment = environment;
            _brandService = brandService;
            _additionalInfoService = additionalInfoService;
            _productImageService = productImageService;
        }

        public async Task<IActionResult> Index(int page = 1, int take = 6)
        {
            IEnumerable<ProductListVM> products = await _productService.GetAllAsync();

            int pageCount = await _productService.GetPageCount(take);

            Paginate<ProductListVM> result = new(products, page, pageCount);

            return View(result);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Brands = await _brandService.GetAllSelectAsync();
            ViewBag.Categories = await _categoryService.GetAllSelectAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductAddVM request)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Brands = await _brandService.GetAllSelectAsync();
                ViewBag.Categories = await _categoryService.GetAllSelectAsync();
                return View(request);
            }

            IEnumerable<ProductListVM> products = await _productService.GetAllAsync();

            if (products.Any(m => m.Name == request.Name))
            {
                ViewBag.Brands = await _brandService.GetAllSelectAsync();
                ViewBag.Categories = await _categoryService.GetAllSelectAsync();

                ModelState.AddModelError("Name", "Name already exists");
                return View(request);
            }

            foreach (var photo in request.Photos)
            {
                if (!photo.CheckFileType("image/"))
                {
                    ViewBag.Brands = await _brandService.GetAllSelectAsync();
                    ViewBag.Categories = await _categoryService.GetAllSelectAsync();

                    ModelState.AddModelError("Photos", "Please, choose correct image type");
                    return View(request);
                }

                if (!photo.CheckFileSize(10000))
                {
                    ViewBag.Brands = await _brandService.GetAllSelectAsync();
                    ViewBag.Categories = await _categoryService.GetAllSelectAsync();

                    ModelState.AddModelError("Photos", "Please, choose correct image size");
                    return View(request);
                }
            }

            var productId = await _productService.AddAsync(request);

            await _additionalInfoService.AddAsync(new AdditionalInfoAddVM()
            { Shades = request.Shades, SkinType = request.SkinType, ProductId = productId });

            foreach (var photo in request.Photos)
            {
                string fileName = Guid.NewGuid().ToString() + "_" + photo.FileName;

                string path = FileExtension.GetFilePath(_environment.WebRootPath, "assets/images", fileName);
                await FileExtension.SaveFile(path, photo);

                await _productImageService.AddAsync(new ProductImageAddVM()
                { Name = fileName, IsMain = photo == request.Photos.First(), ProductId = productId });
            }

            return RedirectToAction(nameof(Index));
        }
    }
}

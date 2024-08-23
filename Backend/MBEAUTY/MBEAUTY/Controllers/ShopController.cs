using AutoMapper;
using MBEAUTY.Helpers;
using MBEAUTY.Services.Interfaces;
using MBEAUTY.ViewModels;
using MBEAUTY.ViewModels.ProductVMs;
using Microsoft.AspNetCore.Mvc;

namespace MBEAUTY.Controllers
{
    public class ShopController : Controller
    {
        private readonly IProductService _productService;
        private readonly IBrandService _brandService;
        private readonly ICategoryService _categoryService;
        private readonly IAdvertService _advertService;
        private readonly IMapper _mapper;

        public ShopController(IProductService productService, IBrandService brandService, ICategoryService categoryService, IAdvertService advertService, IMapper mapper)
        {
            _productService = productService;
            _brandService = brandService;
            _categoryService = categoryService;
            _advertService = advertService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index(ShopVM request)
        {
            IEnumerable<ProductListVM> products = await _productService.GetAllAsync();
            Paginate<ProductListVM> paginateProducts = null;

            if (request.SearchText == null && request.CategoryId == null && request.BrandId == null)
            {
                products = products.Skip((request.Page * request.Take) - request.Take).Take(request.Take);
                paginateProducts = new(products, request.Page,
                    await _productService.GetPageCount(request.Take));
            }
            else
            {
                products = request.SearchText != null
                    ? products.Where(m => m.Name.ToLower().Contains(request.SearchText.ToLower())).ToList()
                    : products.Where(m => m.CategoryId == request.CategoryId || m.BrandId == request.BrandId);
            }

            ShopVM model = new()
            {
                PaginateProducts = paginateProducts,
                Products = products,
                Categories = await _categoryService.GetAllAsync(),
                Brands = await _brandService.GetAllAsync(),
                Advert = await _advertService.GetAsync()
            };

            return View(model);
        }

        public async Task<IActionResult> Detail(int id)
        {
            ProductDetailVM model = _mapper.Map<ProductDetailVM>(await _productService.GetByIdAsync(id));
            model.Products = await _productService.GetAllAsync();

            return View(model);
        }

        public async Task<IActionResult> Search(string searchText)
        {
            IEnumerable<ProductListVM> products = await _productService.GetAllAsync();

            products = searchText != null
                ? products.Where(m => m.Name.ToLower().Contains(searchText.ToLower()))
                : products.Take(6);

            ShopVM model = new() { Products = products };

            return PartialView("_ProductFilterPartial", model);
        }

        public async Task<IActionResult> CategoryFilter(int id)
        {
            IEnumerable<ProductListVM> products = await _productService.GetAllAsync();

            ShopVM model = new() { Products = products.Where(m => m.CategoryId == id) };

            return PartialView("_ProductFilterPartial", model);
        }

        public async Task<IActionResult> BrandFilter(int id)
        {
            IEnumerable<ProductListVM> products = await _productService.GetAllAsync();

            ShopVM model = new() { Products = products.Where(m => m.BrandId == id) };

            return PartialView("_ProductFilterPartial", model);
        }
    }
}

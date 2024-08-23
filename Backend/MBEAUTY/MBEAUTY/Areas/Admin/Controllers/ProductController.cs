using AutoMapper;
using Fruitables_Backend.Helpers;
using MBEAUTY.Helpers;
using MBEAUTY.Models;
using MBEAUTY.Services.Interfaces;
using MBEAUTY.ViewModels.AdditionalInfoVMs;
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
        private readonly IMapper _mapper;

        public ProductController(IProductService productService, ICategoryService categoryService, IWebHostEnvironment environment, IBrandService brandService, IAdditionalInfoService additionalInfoService, IProductImageService productImageService, IMapper mapper)
        {
            _productService = productService;
            _categoryService = categoryService;
            _environment = environment;
            _brandService = brandService;
            _additionalInfoService = additionalInfoService;
            _productImageService = productImageService;
            _mapper = mapper;
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
            ViewBag.Brands = await _brandService.GetAllSelectAsync();
            ViewBag.Categories = await _categoryService.GetAllSelectAsync();

            if (!ModelState.IsValid) return View(request);

            IEnumerable<ProductListVM> products = await _productService.GetAllAsync();

            if (products.Any(m => m.Name == request.Name))
            {
                ModelState.AddModelError("Name", "Name already exists");
                return View(request);
            }

            foreach (var photo in request.Photos)
            {
                if (!photo.CheckFileType("image/"))
                {
                    ModelState.AddModelError("Photos", "Please, choose correct image type");
                    return View(request);
                }

                if (!photo.CheckFileSize(10000))
                {
                    ModelState.AddModelError("Photos", "Please, choose correct image size");
                    return View(request);
                }
            }

            var productId = await _productService.AddAsync(request);

            await _productImageService.AddAsync(productId, request.Photos);

            await _additionalInfoService.AddAsync(new AdditionalInfoAddVM()
            { Shades = request.Shades, SkinType = request.SkinType, ProductId = productId });

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null) return BadRequest();

            ProductDetailVM model = _mapper.Map<ProductDetailVM>(await _productService.GetByIdAsync((int)id));

            if (model == null) return NotFound();

            return View(model);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return BadRequest();

            Product existItem = await _productService.GetByIdAsync((int)id);

            if (existItem == null) return NotFound();

            ViewBag.Brands = await _brandService.GetAllSelectAsync();
            ViewBag.Categories = await _categoryService.GetAllSelectAsync();

            return View(_mapper.Map<ProductEditVM>(existItem));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProductEditVM request)
        {
            ViewBag.Brands = await _brandService.GetAllSelectAsync();
            ViewBag.Categories = await _categoryService.GetAllSelectAsync();

            if (!ModelState.IsValid) return View(request);

            Product existItem = await _productService.GetByIdAsync(id);

            ICollection<ProductImage> productImages = null;

            if (request.Photos != null)
            {
                if (existItem.ProductImages.Count() + request.Photos.Count() > 5)
                {
                    ModelState.AddModelError("Photos", "There should be a maximum of 5 images");
                    request.ProductImages = existItem.ProductImages;
                    return View(request);
                }

                foreach (var photo in request.Photos)
                {
                    if (!photo.CheckFileType("image/"))
                    {
                        ModelState.AddModelError("Photos", "Please, choose correct image type");
                        request.ProductImages = existItem.ProductImages;
                        return View(request);
                    }

                    if (!photo.CheckFileSize(10000))
                    {
                        ModelState.AddModelError("Photos", "Please, choose correct image size");
                        request.ProductImages = existItem.ProductImages;
                        return View(request);
                    }
                }

                productImages = await _productImageService.AddAsync(existItem.Id, request.Photos);
            }

            await _additionalInfoService.UpdateAsync(new AdditionalInfoEditVM()
            { Shades = request.Shades, SkinType = request.SkinType, ProductId = id });

            request.ProductImages = request.Photos == null ? existItem.ProductImages : productImages;
            await _productService.UpdateAsync(request);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteImage(int? id)
        {
            if (id == null) return BadRequest();

            var existItem = await _productImageService.GetByIdAsync((int)id);

            if (existItem.Product.ProductImages.Count == 1 || existItem.IsMain) return Ok(false);

            await _productImageService.DeleteAsync(existItem);
            return Ok(true);
        }

        [HttpPost]
        public async Task<IActionResult> EditImageType(int? id)
        {
            if (id == null) return BadRequest();
            await _productImageService.UpdateType((int)id);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return BadRequest();

            var existProduct = await _productService.GetByIdAsync((int)id);

            foreach (var item in existProduct.ProductImages)
            {
                await _productImageService.DeleteAsync(item);
            }

            await _productService.Delete(existProduct);
            return Ok();
        }
    }
}
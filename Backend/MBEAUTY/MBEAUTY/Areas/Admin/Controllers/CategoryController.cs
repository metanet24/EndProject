using AutoMapper;
using MBEAUTY.Helpers;
using MBEAUTY.Models;
using MBEAUTY.Services.Interfaces;
using MBEAUTY.ViewModels.CategoryVMs;
using Microsoft.AspNetCore.Mvc;

namespace MBEAUTY.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index(int page = 1, int take = 2)
        {
            var items = _mapper.Map<IEnumerable<CategoryListVM>>(await _categoryService.GetAllAsync());
            items = items.Skip((page * take) - take).Take(take);

            var paginateItems = new Paginate<CategoryListVM>
                (items, page, await _categoryService.GetPageCount(take));

            return View(paginateItems);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryAddVM request)
        {
            if (!ModelState.IsValid) return View(request);

            IEnumerable<Category> items = await _categoryService.GetAllAsync();

            if (items.Any(m => m.Name == request.Name))
            {
                ModelState.AddModelError("Name", "Name already exists");
                return View(request);
            }

            await _categoryService.AddAsync(request);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return BadRequest();

            Category existItem = await _categoryService.GetByIdAsync((int)id);

            if (existItem == null) return NotFound();

            return View(_mapper.Map<CategoryEditVM>(existItem));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CategoryEditVM request)
        {
            if (!ModelState.IsValid) return View(request);

            Category existItem = await _categoryService.GetByIdAsync(id);

            await _categoryService.UpdateAsync(request);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return BadRequest();

            var existItem = await _categoryService.GetByIdAsync((int)id);

            await _categoryService.DeleteAsync(existItem);
            return Ok();
        }
    }
}

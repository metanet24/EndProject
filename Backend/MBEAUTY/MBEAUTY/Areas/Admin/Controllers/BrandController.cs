using AutoMapper;
using Fruitables_Backend.Helpers;
using MBEAUTY.Helpers;
using MBEAUTY.Models;
using MBEAUTY.Services.Interfaces;
using MBEAUTY.ViewModels.BrandVMs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MBEAUTY.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin, Admin")]
    public class BrandController : Controller
    {
        private readonly IBrandService _brandService;
        private readonly IMapper _mapper;

        public BrandController(IBrandService brandService, IMapper mapper)
        {
            _brandService = brandService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index(int page = 1, int take = 2)
        {
            var items = _mapper.Map<IEnumerable<BrandListVM>>(await _brandService.GetAllAsync());

            items = items.Skip((page * take) - take).Take(take);

            var paginateItems = new Paginate<BrandListVM>(items, page, await _brandService.GetPageCount(take));

            return View(paginateItems);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BrandAddVM request)
        {
            if (!ModelState.IsValid) return View(request);

            IEnumerable<Brand> items = await _brandService.GetAllAsync();

            if (items.Any(m => m.Name == request.Name))
            {
                ModelState.AddModelError("Name", "Name already exists");
                return View(request);
            }

            if (!request.Photo.CheckFileType("image/"))
            {
                ModelState.AddModelError("Photo", "Please, choose correct image type");
                return View(request);
            }

            if (!request.Photo.CheckFileSize(10000))
            {
                ModelState.AddModelError("Photo", "Please, choose correct image size");
                return View(request);
            }

            await _brandService.AddAsync(request);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return BadRequest();

            Brand existItem = await _brandService.GetByIdAsync((int)id);

            if (existItem == null) return NotFound();

            return View(_mapper.Map<BrandEditVM>(existItem));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, BrandEditVM request)
        {
            if (!ModelState.IsValid) return View(request);

            Brand existItem = await _brandService.GetByIdAsync(id);

            if (request.Photo != null)
            {
                if (!request.Photo.CheckFileType("image/"))
                {
                    ModelState.AddModelError("Photo", "Please, choose correct image type");
                    return View(request);
                }

                if (!request.Photo.CheckFileSize(10000))
                {
                    ModelState.AddModelError("Photo", "Please, choose correct image size");
                    return View(request);
                }
            }

            await _brandService.UpdateAsync(request);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return BadRequest();

            var existItem = await _brandService.GetByIdAsync((int)id);

            await _brandService.DeleteAsync(existItem);
            return Ok();
        }
    }
}

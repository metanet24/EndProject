using AutoMapper;
using Fruitables_Backend.Helpers;
using MBEAUTY.Helpers;
using MBEAUTY.Models;
using MBEAUTY.Services.Interfaces;
using MBEAUTY.ViewModels.FamousVms;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MBEAUTY.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin, Admin")]
    public class FamousController : Controller
    {
        private readonly IFamousService _famousService;
        private readonly IMapper _mapper;

        public FamousController(IFamousService famousService, IMapper mapper)
        {
            _famousService = famousService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index(int page = 1, int take = 2)
        {
            var items = _mapper.Map<IEnumerable<FamousListVM>>(await _famousService.GetAllAsync());
            items = items.Skip((page * take) - take).Take(take);

            var paginateItems = new Paginate<FamousListVM>(items, page, await _famousService.GetPageCount(take));

            return View(paginateItems);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FamousAddVM request)
        {
            if (!ModelState.IsValid) return View(request);

            IEnumerable<Famous> items = await _famousService.GetAllAsync();

            if (items.Any(m => m.FullName == request.FullName))
            {
                ModelState.AddModelError("FullName", "Name already exists");
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

            await _famousService.AddAsync(request);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return BadRequest();

            Famous existItem = await _famousService.GetByIdAsync((int)id);

            if (existItem == null) return NotFound();

            return View(_mapper.Map<FamousEditVM>(existItem));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, FamousEditVM request)
        {
            if (!ModelState.IsValid) return View(request);

            Famous existItem = await _famousService.GetByIdAsync(id);

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

            await _famousService.UpdateAsync(request);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return BadRequest();

            var existItem = await _famousService.GetByIdAsync((int)id);

            await _famousService.DeleteAsync(existItem);
            return Ok();
        }
    }
}

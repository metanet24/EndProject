using AutoMapper;
using Fruitables_Backend.Helpers;
using MBEAUTY.Models;
using MBEAUTY.Services.Interfaces;
using MBEAUTY.ViewModels.AdvertVMs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MBEAUTY.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin, Admin")]
    public class AdvertController : Controller
    {
        private readonly IAdvertService _advertService;
        private readonly IMapper _mapper;

        public AdvertController(IAdvertService advertService, IMapper mapper)
        {
            _advertService = advertService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<AdvertVM>(await _advertService.GetAsync()));
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AdvertAddVM request)
        {
            if (!ModelState.IsValid) return View(request);

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

            await _advertService.AddAsync(request);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return BadRequest();

            Advert existItem = await _advertService.GetByIdAsync((int)id);

            if (existItem == null) return NotFound();

            return View(_mapper.Map<AdvertEditVM>(existItem));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, AdvertEditVM request)
        {
            if (!ModelState.IsValid) return View(request);

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

            await _advertService.UpdateAsync(request);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return BadRequest();

            var existItem = await _advertService.GetByIdAsync((int)id);

            await _advertService.DeleteAsync(existItem);
            return Ok();
        }
    }
}

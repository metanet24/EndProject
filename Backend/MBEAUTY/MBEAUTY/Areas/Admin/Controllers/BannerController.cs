using AutoMapper;
using Fruitables_Backend.Helpers;
using MBEAUTY.Models;
using MBEAUTY.Services.Interfaces;
using MBEAUTY.ViewModels.BannnerVMs;
using Microsoft.AspNetCore.Mvc;

namespace MBEAUTY.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BannerController : Controller
    {
        private readonly IBannerService _bannerService;
        private readonly IMapper _mapper;

        public BannerController(IBannerService bannerService, IMapper mapper)
        {
            _bannerService = bannerService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<BannerVM>(await _bannerService.GetAsync()));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return BadRequest();

            Banner existItem = await _bannerService.GetByIdAsync((int)id);

            if (existItem == null) return NotFound();

            return View(_mapper.Map<BannerEditVM>(existItem));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, BannerEditVM request)
        {
            if (!ModelState.IsValid) return View(request);

            Banner existItem = await _bannerService.GetByIdAsync(id);

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

            await _bannerService.UpdateAsync(request);

            return RedirectToAction(nameof(Index));
        }
    }
}

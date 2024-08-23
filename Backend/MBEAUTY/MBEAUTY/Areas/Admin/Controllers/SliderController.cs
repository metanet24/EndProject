using AutoMapper;
using Fruitables_Backend.Helpers;
using MBEAUTY.Models;
using MBEAUTY.Services.Interfaces;
using MBEAUTY.ViewModels.SliderVMs;
using Microsoft.AspNetCore.Mvc;

namespace MBEAUTY.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SliderController : Controller
    {
        private readonly ISliderService _sliderService;
        private readonly IMapper _mapper;

        public SliderController(ISliderService sliderService, IMapper mapper)
        {
            _sliderService = sliderService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<SliderListVM>>(await _sliderService.GetAllAsync()));
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SliderAddVM request)
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

            await _sliderService.AddAsync(request);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return BadRequest();

            Slider existItem = await _sliderService.GetByIdAsync((int)id);

            if (existItem == null) return NotFound();

            return View(_mapper.Map<SliderEditVM>(existItem));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SliderEditVM request)
        {
            if (!ModelState.IsValid) return View(request);

            Slider existItem = await _sliderService.GetByIdAsync(id);

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

            await _sliderService.UpdateAsync(request);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return BadRequest();

            var existItem = await _sliderService.GetByIdAsync((int)id);

            await _sliderService.DeleteAsync(existItem);
            return Ok();
        }
    }
}

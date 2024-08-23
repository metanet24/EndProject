using AutoMapper;
using MBEAUTY.Models;
using MBEAUTY.Services.Interfaces;
using MBEAUTY.ViewModels.SettingVMs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MBEAUTY.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin, Admin")]
    public class SettingController : Controller
    {
        private readonly ISettingService _settingService;
        private readonly IMapper _mapper;

        public SettingController(ISettingService settingService, IMapper mapper)
        {
            _settingService = settingService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<SettingListVM>>(await _settingService.GetAllAsync()));
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SettingAddVM request)
        {
            if (!ModelState.IsValid) return View(request);

            IEnumerable<Setting> items = await _settingService.GetAllAsync();

            if (items.Any(m => m.Key == request.Key))
            {
                ModelState.AddModelError("Key", "Key already exists");
                return View(request);
            }

            await _settingService.AddAsync(request);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return BadRequest();

            Setting existItem = await _settingService.GetByIdAsync((int)id);

            if (existItem == null) return NotFound();

            return View(_mapper.Map<SettingEditVM>(existItem));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SettingEditVM request)
        {
            if (!ModelState.IsValid) return View(request);

            Setting existItem = await _settingService.GetByIdAsync(id);

            await _settingService.UpdateAsync(request);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return BadRequest();

            var existItem = await _settingService.GetByIdAsync((int)id);

            await _settingService.DeleteAsync(existItem);
            return Ok();
        }
    }
}

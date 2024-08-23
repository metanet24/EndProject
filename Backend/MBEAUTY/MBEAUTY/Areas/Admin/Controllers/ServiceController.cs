using AutoMapper;
using MBEAUTY.Models;
using MBEAUTY.Services.Interfaces;
using MBEAUTY.ViewModels.ServicesVMs;
using Microsoft.AspNetCore.Mvc;

namespace MBEAUTY.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ServiceController : Controller
    {
        private readonly IServiceService _serviceService;
        private readonly IMapper _mapper;

        public ServiceController(IServiceService serviceService, IMapper mapper)
        {
            _serviceService = serviceService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<ServiceListVM>>(await _serviceService.GetAllAsync()));
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ServiceAddVM request)
        {
            if (!ModelState.IsValid) return View(request);

            IEnumerable<Service> items = await _serviceService.GetAllAsync();

            if (items.Any(m => m.Title == request.Title))
            {
                ModelState.AddModelError("Title", "Name already exists");
                return View(request);
            }

            await _serviceService.AddAsync(request);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return BadRequest();

            Service existItem = await _serviceService.GetByIdAsync((int)id);

            if (existItem == null) return NotFound();

            return View(_mapper.Map<ServiceEditVM>(existItem));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ServiceEditVM request)
        {
            if (!ModelState.IsValid) return View(request);

            Service existItem = await _serviceService.GetByIdAsync(id);

            await _serviceService.UpdateAsync(request);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return BadRequest();

            var existItem = await _serviceService.GetByIdAsync((int)id);

            await _serviceService.DeleteAsync(existItem);
            return Ok();
        }
    }
}

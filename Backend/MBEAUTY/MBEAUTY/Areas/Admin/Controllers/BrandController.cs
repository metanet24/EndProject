using AutoMapper;
using MBEAUTY.Helpers;
using MBEAUTY.Services.Interfaces;
using MBEAUTY.ViewModels.BrandVMs;
using Microsoft.AspNetCore.Mvc;

namespace MBEAUTY.Areas.Admin.Controllers
{
    [Area("Admin")]
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
    }
}

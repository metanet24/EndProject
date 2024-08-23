using AutoMapper;
using MBEAUTY.Services.Interfaces;
using MBEAUTY.ViewModels.AboutVms;
using MBEAUTY.ViewModels.FamousVms;
using Microsoft.AspNetCore.Mvc;

namespace MBEAUTY.Controllers
{
    public class AboutController : Controller
    {
        private readonly IAboutService _aboutService;
        private readonly IFamousService _famousService;
        private readonly IMapper _mapper;

        public AboutController(IAboutService aboutService, IFamousService famousService, IMapper mapper)
        {
            _aboutService = aboutService;
            _famousService = famousService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            AboutVM model = _mapper.Map<AboutVM>(await _aboutService.GetAsync());
            model.Famous = _mapper.Map<IEnumerable<FamousListVM>>(await _famousService.GetAllAsync());

            return View(model);
        }
    }
}

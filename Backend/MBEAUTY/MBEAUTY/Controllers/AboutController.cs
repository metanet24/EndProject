using MBEAUTY.Services.Interfaces;
using MBEAUTY.ViewModels.AboutVms;
using Microsoft.AspNetCore.Mvc;

namespace MBEAUTY.Controllers
{
    public class AboutController : Controller
    {
        private readonly IAboutService _aboutService;
        private readonly IFamousService _famousService;

        public AboutController(IAboutService aboutService, IFamousService famousService)
        {
            _aboutService = aboutService;
            _famousService = famousService;
        }

        public async Task<IActionResult> Index()
        {
            AboutVM model = await _aboutService.GetAsync();
            model.Famous = await _famousService.GetAllAsync();

            return View(model);
        }
    }
}

using MBEAUTY.Models;
using MBEAUTY.Services.Interfaces;
using MBEAUTY.ViewModels.ContactVMs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MBEAUTY.Controllers
{
    public class ContactController : Controller
    {
        private readonly IContactService _contactService;
        private readonly UserManager<AppUser> _userManager;

        public ContactController(UserManager<AppUser> userManager, IContactService contactService)
        {
            _contactService = contactService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            ContactAddVM model = new();

            if (User.Identity.IsAuthenticated)
            {
                AppUser user = new();
                user = await _userManager.FindByNameAsync(User.Identity.Name);

                model.FullName = user.FullName;
                model.Email = user.Email;
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Index(ContactAddVM request)
        {
            AppUser user = new();

            if (User.Identity.IsAuthenticated) user = await _userManager.FindByNameAsync(User.Identity.Name);

            if (!ModelState.IsValid) return View(request);

            await _contactService.AddAsync(request);
            return Ok();
        }
    }
}

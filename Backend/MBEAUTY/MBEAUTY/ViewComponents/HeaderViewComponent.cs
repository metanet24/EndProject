using MBEAUTY.Models;
using MBEAUTY.Services.Interfaces;
using MBEAUTY.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MBEAUTY.ViewComponents
{
    public class HeaderViewComponent : ViewComponent
    {
        private readonly ISettingService _settingService;
        private readonly IBasketService _basketService;
        private readonly UserManager<AppUser> _userManager;


        public HeaderViewComponent(ISettingService settingService, IBasketService basketService, UserManager<AppUser> userManager)
        {
            _settingService = settingService;
            _basketService = basketService;
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            AppUser existUser = new();

            if (User.Identity.IsAuthenticated)
            {
                existUser = await _userManager.FindByNameAsync(User.Identity?.Name);
            }

            HeaderVM model = new()
            {
                Settings = await _settingService.GetAllDictionaryAsync(),
                BasketCount = await _basketService.GetCountByAppUserIdAsync(existUser.Id)
            };

            return await Task.FromResult(View(model));
        }
    }
}

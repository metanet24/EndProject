using MBEAUTY.Services.Interfaces;
using MBEAUTY.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace MBEAUTY.ViewComponents
{
    public class HeaderViewComponent : ViewComponent
    {
        private readonly ISettingService _settingService;

        public HeaderViewComponent(ISettingService settingService)
        {
            _settingService = settingService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            HeaderVM model = new()
            {
                Settings = await _settingService.GetAllAsync(),
            };

            return await Task.FromResult(View(model));
        }
    }
}

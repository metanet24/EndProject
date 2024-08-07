using MBEAUTY.Services.Interfaces;
using MBEAUTY.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace MBEAUTY.ViewComponents
{
    public class FooterViewComponent : ViewComponent
    {
        private readonly ISettingService _settingService;

        public FooterViewComponent(ISettingService settingService)
        {
            _settingService = settingService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            FooterVM model = new()
            {
                Settings = await _settingService.GetAllAsync(),
            };

            return await Task.FromResult(View(model));
        }
    }
}
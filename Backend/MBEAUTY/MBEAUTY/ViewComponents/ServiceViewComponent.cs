using MBEAUTY.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MBEAUTY.ViewComponents
{
    public class ServiceViewComponent : ViewComponent
    {
        private readonly IServiceService _serviceService;

        public ServiceViewComponent(IServiceService serviceService)
        {
            _serviceService = serviceService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult(View(await _serviceService.GetAllAsync()));
        }
    }
}

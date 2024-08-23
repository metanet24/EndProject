using AutoMapper;
using MBEAUTY.Services.Interfaces;
using MBEAUTY.ViewModels.ServicesVMs;
using Microsoft.AspNetCore.Mvc;

namespace MBEAUTY.ViewComponents
{
    public class ServiceViewComponent : ViewComponent
    {
        private readonly IServiceService _serviceService;
        private readonly IMapper _mapper;

        public ServiceViewComponent(IServiceService serviceService, IMapper mapper)
        {
            _serviceService = serviceService;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult(View(_mapper.Map<IEnumerable<ServiceListVM>>(
                await _serviceService.GetAllAsync())));
        }
    }
}

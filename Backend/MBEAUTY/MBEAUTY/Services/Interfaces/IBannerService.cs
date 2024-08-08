using MBEAUTY.Models;
using MBEAUTY.ViewModels.BannnerVMs;

namespace MBEAUTY.Services.Interfaces
{
    public interface IBannerService
    {
        Task UpdateAsync(Banner model);
        Task SaveAsync();
        Task<BannerVM> GetAsync();
    }
}

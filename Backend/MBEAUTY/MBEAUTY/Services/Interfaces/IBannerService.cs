using MBEAUTY.Models;
using MBEAUTY.ViewModels.BannnerVMs;

namespace MBEAUTY.Services.Interfaces
{
    public interface IBannerService
    {
        Task UpdateAsync(BannerEditVM item);
        Task<Banner> GetAsync();
        Task<Banner> GetByIdAsync(int id);
    }
}

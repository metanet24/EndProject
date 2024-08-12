using MBEAUTY.Models;
using MBEAUTY.ViewModels.AboutVms;

namespace MBEAUTY.Services.Interfaces
{
    public interface IAboutService
    {
        Task UpdateAsync(About model);
        Task<AboutVM> GetAsync();
        Task SaveAsync();
    }
}

using MBEAUTY.Models;
using MBEAUTY.ViewModels.AboutVms;

namespace MBEAUTY.Services.Interfaces
{
    public interface IAboutService
    {
        Task UpdateAsync(AboutEditVM item);
        Task<About> GetAsync();
        Task<About> GetByIdAsync(int id);
    }
}

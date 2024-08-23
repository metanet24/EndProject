using MBEAUTY.Models;
using MBEAUTY.ViewModels.AdvertVMs;

namespace MBEAUTY.Services.Interfaces
{
    public interface IAdvertService
    {
        Task AddAsync(AdvertAddVM item);
        Task UpdateAsync(AdvertEditVM item);
        Task DeleteAsync(Advert item);
        Task<Advert> GetAsync();
        Task<Advert> GetByIdAsync(int id);
    }
}

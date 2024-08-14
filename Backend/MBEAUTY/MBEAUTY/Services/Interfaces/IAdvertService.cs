using MBEAUTY.Models;
using MBEAUTY.ViewModels.AdvertVMs;

namespace MBEAUTY.Services.Interfaces
{
    public interface IAdvertService
    {
        Task AddAsync(Advert advert);
        void Delete(Advert advert);
        Task SaveAsync();
        Task<AdvertVM> GetAsync();
        Task<Advert> GetByIdAsync(int id);
    }
}

using MBEAUTY.Models;
using MBEAUTY.ViewModels.SliderVMs;

namespace MBEAUTY.Services.Interfaces
{
    public interface ISliderService
    {
        Task AddAsync(Slider slider);
        void Delete(Slider slider);
        Task<IEnumerable<SliderListVM>> GetAllAsync();
        Task<Slider> GetByIdAsync(int id);
        Task SaveChangesAsync();
    }
}

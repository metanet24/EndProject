using MBEAUTY.Models;
using MBEAUTY.ViewModels.SliderVMs;

namespace MBEAUTY.Services.Interfaces
{
    public interface ISliderService
    {
        Task AddAsync(SliderAddVM item);
        Task UpdateAsync(SliderEditVM item);
        Task DeleteAsync(Slider item);
        Task<IEnumerable<Slider>> GetAllAsync();
        Task<Slider> GetByIdAsync(int id);
    }
}

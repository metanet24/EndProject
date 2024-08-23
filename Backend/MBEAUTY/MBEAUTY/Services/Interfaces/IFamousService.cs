using MBEAUTY.Models;
using MBEAUTY.ViewModels.FamousVms;

namespace MBEAUTY.Services.Interfaces
{
    public interface IFamousService
    {
        Task AddAsync(FamousAddVM item);
        Task UpdateAsync(FamousEditVM item);
        Task DeleteAsync(Famous item);
        Task<IEnumerable<Famous>> GetAllAsync();
        Task<Famous> GetByIdAsync(int id);
        Task<int> GetPageCount(int take);
    }
}

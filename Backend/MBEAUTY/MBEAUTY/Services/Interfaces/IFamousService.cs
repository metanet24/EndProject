using MBEAUTY.Models;
using MBEAUTY.ViewModels.FamousVms;

namespace MBEAUTY.Services.Interfaces
{
    public interface IFamousService
    {
        Task AddAsync(Famous famous);
        void Delete(Famous famous);
        Task<IEnumerable<FamousListVM>> GetAllAsync();
        Task<Famous> GetByIdAsync(int id);
        Task SaveAsync();
    }
}

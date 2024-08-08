using MBEAUTY.Models;
using MBEAUTY.ViewModels.BrandVMs;

namespace MBEAUTY.Services.Interfaces
{
    public interface IBrandService
    {
        Task AddAsync(Brand product);
        void Delete(Brand product);
        Task SaveAsync();
        Task<IEnumerable<BrandListVM>> GetAllAsync();
        Task<Product> GetByIdAsync(int id);
    }
}

using MBEAUTY.Models;
using MBEAUTY.ViewModels.BrandVMs;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MBEAUTY.Services.Interfaces
{
    public interface IBrandService
    {
        Task AddAsync(BrandAddVM item);
        Task UpdateAsync(BrandEditVM item);
        Task DeleteAsync(Brand item);
        Task<IEnumerable<Brand>> GetAllAsync();
        Task<SelectList> GetAllSelectAsync();
        Task<Brand> GetByIdAsync(int id);
        Task<int> GetPageCount(int take);
    }
}

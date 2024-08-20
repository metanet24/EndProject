using MBEAUTY.Models;
using MBEAUTY.ViewModels.BrandVMs;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MBEAUTY.Services.Interfaces
{
    public interface IBrandService
    {
        Task AddAsync(Brand product);
        void Delete(Brand product);
        Task SaveAsync();
        Task<IEnumerable<BrandListVM>> GetAllAsync();
        Task<SelectList> GetAllSelectAsync();
        Task<Product> GetByIdAsync(int id);
    }
}

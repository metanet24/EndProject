using MBEAUTY.Models;
using MBEAUTY.ViewModels.CategoryVMs;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MBEAUTY.Services.Interfaces
{
    public interface ICategoryService
    {
        Task AddAsync(Category category);
        void Delete(Category category);
        Task<IEnumerable<CategoryListVM>> GetAllAsync();
        Task<SelectList> GetAllSelectAsync();
        Task<Category> GetByIdAsync(int id);
        Task SaveAsync();
    }
}

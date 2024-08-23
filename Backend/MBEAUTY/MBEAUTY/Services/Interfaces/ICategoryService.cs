using MBEAUTY.Models;
using MBEAUTY.ViewModels.CategoryVMs;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MBEAUTY.Services.Interfaces
{
    public interface ICategoryService
    {
        Task AddAsync(CategoryAddVM category);
        Task UpdateAsync(CategoryEditVM category);
        Task DeleteAsync(Category category);
        Task<IEnumerable<Category>> GetAllAsync();
        Task<SelectList> GetAllSelectAsync();
        Task<Category> GetByIdAsync(int id);
        Task<int> GetPageCount(int take);
    }
}

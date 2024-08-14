using MBEAUTY.Models;
using MBEAUTY.ViewModels.CategoryVMs;

namespace MBEAUTY.Services.Interfaces
{
    public interface ICategoryService
    {
        Task AddAsync(Category category);
        void Delete(Category category);
        Task<IEnumerable<CategoryListVM>> GetAllAsync();
        Task<Category> GetByIdAsync(int id);
        Task SaveAsync();
    }
}

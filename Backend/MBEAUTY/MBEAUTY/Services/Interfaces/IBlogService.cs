using MBEAUTY.Models;
using MBEAUTY.ViewModels.BlogVMs;

namespace MBEAUTY.Services.Interfaces
{
    public interface IBlogService
    {
        Task<int> AddAsync(BlogAddVM blog);
        Task UpdateAsync(BlogEditVM blog);
        Task DeleteAsync(Blog blog);
        Task<IEnumerable<Blog>> GetAllAsync();
        Task<Blog> GetByIdAsync(int id);
        Task<int> GetPageCount(int take);
    }
}

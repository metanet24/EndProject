using MBEAUTY.Models;
using MBEAUTY.ViewModels.BlogVMs;

namespace MBEAUTY.Services.Interfaces
{
    public interface IBlogService
    {
        Task AddAsync(Blog blog);
        void Delete(Blog blog);
        Task<IEnumerable<BlogListVM>> GetAllAsync();
        Task<Blog> GetByIdAsync(int id);
        Task SaveAsync();
    }
}

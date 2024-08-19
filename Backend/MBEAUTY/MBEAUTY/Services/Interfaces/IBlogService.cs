using MBEAUTY.Models;
using MBEAUTY.ViewModels.BlogVMs;

namespace MBEAUTY.Services.Interfaces
{
    public interface IBlogService
    {
        Task AddAsync(Blog blog);
        void Delete(Blog blog);
        Task<IEnumerable<BlogListVM>> GetAllAsync();
        Task<BlogDetailVM> GetByIdAsync(int id);
        Task SaveAsync();
    }
}

using MBEAUTY.Models;

namespace MBEAUTY.Services.Interfaces
{
    public interface IBlogImageService
    {
        Task<ICollection<BlogImage>> GetAllByBlogId(int blogId);
        Task<ICollection<BlogImage>> AddAsync(int blogId, IFormFile[] photos);
        Task<BlogImage> GetByIdAsync(int id);
        Task UpdateType(int id);
        Task DeleteAsync(BlogImage blogImage);
    }
}

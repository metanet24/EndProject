using MBEAUTY.Models;

namespace MBEAUTY.Services.Interfaces
{
    public interface IProductImageService
    {
        Task<ICollection<ProductImage>> GetAllByProductId(int productId);
        Task<ICollection<ProductImage>> AddAsync(int productId, IFormFile[] photos);
        Task<ProductImage> GetByIdAsync(int id);
        Task UpdateType(int id);
        Task DeleteAsync(ProductImage productImage);
    }
}

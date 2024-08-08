using MBEAUTY.Models;
using MBEAUTY.ViewModels.ProductVMs;

namespace MBEAUTY.Services.Interfaces
{
    public interface IProductService
    {
        Task AddAsync(Product product);
        void Delete(Product product);
        Task SaveAsync();
        Task<IEnumerable<ProductListVM>> GetAllAsync();
        Task<Product> GetByIdAsync(int id);
    }
}

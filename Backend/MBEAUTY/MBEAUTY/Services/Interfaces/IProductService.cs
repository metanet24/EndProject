using MBEAUTY.Models;
using MBEAUTY.ViewModels.ProductVMs;

namespace MBEAUTY.Services.Interfaces
{
    public interface IProductService
    {
        Task<int> AddAsync(ProductAddVM product);
        Task UpdateAsync(ProductEditVM product);
        Task Delete(Product product);
        Task<IEnumerable<ProductListVM>> GetAllAsync();
        Task<Product> GetByIdAsync(int id);
        Task<int> GetPageCount(int take);
    }
}

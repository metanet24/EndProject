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
        Task<ProductDetailVM> GetByIdAsync(int id);
        Task<int> GetPageCount(int take);
    }
}

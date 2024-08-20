using MBEAUTY.ViewModels.ProductImageVMs;

namespace MBEAUTY.Services.Interfaces
{
    public interface IProductImageService
    {
        Task AddAsync(ProductImageAddVM productImage);
        //void Delete(int id);
        //Task<IEnumerable<ProductListVM>> GetAllAsync();
        //Task<ProductDetailVM> GetByIdAsync(int id);
    }
}

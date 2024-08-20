using MBEAUTY.Models;
using MBEAUTY.ViewModels.BasketVMs;

namespace MBEAUTY.Services.Interfaces
{
    public interface IBasketService
    {
        Task<int> AddAsync(BasketAddVM basket);
        Task AddBasketProductAsync(BasketProductAddVM basketProduct);
        void DeleteBasketProduct(BasketProduct basketProduct);
        Task<bool> AddBasketProductByAppUserIdAsync(string appUserId, int productId);
        Task<int> GetCountByAppUserIdAsync(string appUserId);
        Task<decimal> GetTotalByAppUserIdAsync(string appUserId);
        Task<BasketProduct> GetBasketProductByIdAsync(int id);
        Task<bool> BasketProductPlusByProductIdAndAppUserIdAsync(int productId, string appUserId);
    }
}

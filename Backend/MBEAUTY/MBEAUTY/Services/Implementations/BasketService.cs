using AutoMapper;
using MBEAUTY.Data;
using MBEAUTY.Models;
using MBEAUTY.Services.Interfaces;
using MBEAUTY.ViewModels.BasketVMs;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace MBEAUTY.Services.Implementations
{
    public class BasketService : IBasketService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public BasketService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<bool> BasketProductPlusByProductIdAndAppUserIdAsync(int productId, string appUserId)
        {
            var basketProduct = await _context.BasketProducts
                .FirstOrDefaultAsync(m => m.ProductId == productId && m.Basket.AppUserId == appUserId);

            if (basketProduct != null)
            {
                basketProduct.Quantity++;
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<bool> AddBasketProductByAppUserIdAsync(string appUserId, int productId)
        {
            var basket = await _context.Baskets
                .Include(m => m.BasketProducts)
                .ThenInclude(m => m.Product)
                .ThenInclude(m => m.ProductImages)
                .FirstOrDefaultAsync(m => m.AppUserId == appUserId);

            if (basket != null)
            {
                basket.BasketProducts.Add(new BasketProduct()
                {
                    ProductId = productId,
                    Quantity = 1
                });

                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<int> AddAsync(BasketAddVM basket)
        {
            var newBasket = _mapper.Map<Basket>(basket);

            await _context.Baskets.AddAsync(newBasket);
            await _context.SaveChangesAsync();

            return newBasket.Id;
        }

        public async Task AddBasketProductAsync(BasketProductAddVM basketProduct)
        {
            await _context.BasketProducts.AddAsync(_mapper.Map<BasketProduct>(basketProduct));
            await _context.SaveChangesAsync();
        }

        public async void DeleteBasketProduct(BasketProduct basketProduct)
        {
            _context.BasketProducts.Remove(basketProduct);
            await _context.SaveChangesAsync();
        }

        public async Task<int> GetCountByAppUserIdAsync(string appUserId)
        {
            return await _context.BasketProducts.Where(m => m.Basket.AppUserId == appUserId)
                 .SumAsync(m => m.Quantity);
        }

        public async Task<decimal> GetTotalByAppUserIdAsync(string appUserId)
        {
            return await _context.BasketProducts.Where(m => m.Basket.AppUserId == appUserId)
                 .SumAsync(m => m.Product.Price * m.Quantity);
        }

        public async Task<BasketProduct> GetBasketProductByIdAsync(int id)
        {
            return await _context.BasketProducts.Include(m => m.Product).FirstOrDefaultAsync(m => m.Id == id);
        }
    }
}

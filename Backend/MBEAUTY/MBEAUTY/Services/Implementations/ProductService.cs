using AutoMapper;
using MBEAUTY.Data;
using MBEAUTY.Models;
using MBEAUTY.Services.Interfaces;
using MBEAUTY.ViewModels.ProductVMs;
using Microsoft.EntityFrameworkCore;

namespace MBEAUTY.Services.Implementations
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ProductService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> AddAsync(ProductAddVM product)
        {
            var newProduct = _mapper.Map<Product>(product);

            await _context.Products.AddAsync(newProduct);
            await _context.SaveChangesAsync();

            return newProduct.Id;
        }

        public async Task UpdateAsync(ProductEditVM product)
        {
            var existItem = await GetByIdAsync(product.Id);

            _context.Products.Update(_mapper.Map(product, existItem));
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Product product)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Products
                .Where(m => !m.SoftDeleted)
                .Include(m => m.ProductImages)
                .Include(m => m.Category)
                .Include(m => m.Brand)
                .OrderByDescending(m => m.Id)
                .ToListAsync();
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            return await _context.Products
                .Where(m => !m.SoftDeleted)
                .Include(m => m.ProductImages)
                .Include(m => m.Category)
                .Include(m => m.Brand)
                .Include(m => m.AdditionalInfo)
                .OrderByDescending(m => m.Id)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<int> GetPageCount(int take)
        {
            IEnumerable<Product> products = await GetAllAsync();

            return (int)Math.Ceiling((decimal)products.Count() / take);
        }
    }
}

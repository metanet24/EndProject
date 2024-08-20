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

        public async Task AddAsync(Product product)
        {
            await _context.Products.AddAsync(product);
        }

        public void Delete(Product product)
        {
            _context.Products.Remove(product);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ProductListVM>> GetAllAsync()
        {
            IEnumerable<Product> products = await _context.Products
                .Where(m => !m.SoftDeleted)
                .Include(m => m.ProductImages)
                .Include(m => m.Category)
                .Include(m => m.Brand)
                .OrderByDescending(m => m.Id)
                .ToListAsync();

            return _mapper.Map<IEnumerable<ProductListVM>>(products);
        }

        public async Task<ProductDetailVM> GetByIdAsync(int id)
        {
            Product product = await _context.Products
                .Where(m => !m.SoftDeleted)
                .Include(m => m.ProductImages)
                .Include(m => m.Category)
                .Include(m => m.Brand)
                .Include(m => m.AdditionalInfo)
                .OrderByDescending(m => m.Id)
                .FirstOrDefaultAsync(m => m.Id == id);

            return _mapper.Map<ProductDetailVM>(product);
        }

        public async Task<int> GetPageCount(int take)
        {
            IEnumerable<ProductListVM> products = await GetAllAsync();

            return (int)Math.Ceiling((decimal)products.Count() / take);
        }
    }
}

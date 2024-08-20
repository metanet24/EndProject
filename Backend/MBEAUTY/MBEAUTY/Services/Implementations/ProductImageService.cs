using AutoMapper;
using MBEAUTY.Data;
using MBEAUTY.Models;
using MBEAUTY.Services.Interfaces;
using MBEAUTY.ViewModels.ProductImageVMs;

namespace MBEAUTY.Services.Implementations
{
    public class ProductImageService : IProductImageService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ProductImageService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task AddAsync(ProductImageAddVM productImage)
        {
            await _context.ProductImages.AddAsync(_mapper.Map<ProductImage>(productImage));
            await _context.SaveChangesAsync();
        }
    }
}

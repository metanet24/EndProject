using AutoMapper;
using Fruitables_Backend.Helpers;
using MBEAUTY.Data;
using MBEAUTY.Models;
using MBEAUTY.Services.Interfaces;
using MBEAUTY.ViewModels.ProductImageVMs;
using Microsoft.EntityFrameworkCore;

namespace MBEAUTY.Services.Implementations
{
    public class ProductImageService : IProductImageService
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _environment;
        private readonly IMapper _mapper;

        public ProductImageService(AppDbContext context, IMapper mapper, IWebHostEnvironment environment)
        {
            _context = context;
            _mapper = mapper;
            _environment = environment;
        }

        public async Task<ICollection<ProductImage>> AddAsync(int productId, IFormFile[] photos)
        {
            foreach (var photo in photos)
            {
                string fileName = Guid.NewGuid().ToString() + "_" + photo.FileName;

                string path = FileExtension.GetFilePath(_environment.WebRootPath, "assets/images", fileName);
                await FileExtension.SaveFile(path, photo);

                var newItem = new ProductImageAddVM()
                { Name = fileName, IsMain = photo == photos.First(), ProductId = productId };

                await _context.ProductImages.AddAsync(_mapper.Map<ProductImage>(newItem));
                await _context.SaveChangesAsync();
            }

            return await GetAllByProductId(productId);
        }

        public async Task DeleteAsync(ProductImage productImage)
        {
            string path = FileExtension
                .GetFilePath(_environment.WebRootPath, "assets/images", productImage.Name);

            FileExtension.DeleteFile(path);

            _context.ProductImages.Remove(productImage);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateType(int id)
        {
            var existItem = await GetByIdAsync(id);

            foreach (var item in existItem.Product.ProductImages) item.IsMain = false;

            existItem.IsMain = true;

            await _context.SaveChangesAsync();
        }

        public async Task<ProductImage> GetByIdAsync(int id)
        {
            return await _context.ProductImages.Include(m => m.Product)
                .ThenInclude(m => m.ProductImages).FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<ICollection<ProductImage>> GetAllByProductId(int productId)
        {
            return await _context.ProductImages.Where(m => m.ProductId == productId).ToListAsync();
        }
    }
}

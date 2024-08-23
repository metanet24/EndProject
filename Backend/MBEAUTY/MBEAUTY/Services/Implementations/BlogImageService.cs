using AutoMapper;
using Fruitables_Backend.Helpers;
using MBEAUTY.Data;
using MBEAUTY.Models;
using MBEAUTY.Services.Interfaces;
using MBEAUTY.ViewModels.BlogImageVMs;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace MBEAUTY.Services.Implementations
{
    public class BlogImageService : IBlogImageService
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _environment;
        private readonly IMapper _mapper;

        public BlogImageService(AppDbContext context, IMapper mapper, IWebHostEnvironment environment)
        {
            _context = context;
            _mapper = mapper;
            _environment = environment;
        }

        public async Task<ICollection<BlogImage>> AddAsync(int blogId, IFormFile[] photos)
        {
            foreach (var photo in photos)
            {
                string fileName = Guid.NewGuid().ToString() + "_" + photo.FileName;

                string path = FileExtension.GetFilePath(_environment.WebRootPath, "assets/images", fileName);
                await FileExtension.SaveFile(path, photo);

                var newItem = new BlogImageAddVM()
                { Name = fileName, IsMain = photo == photos.First(), BlogId = blogId };

                await _context.BlogImages.AddAsync(_mapper.Map<BlogImage>(newItem));
                await _context.SaveChangesAsync();
            }

            return await GetAllByBlogId(blogId);
        }

        public async Task DeleteAsync(BlogImage blogImage)
        {
            string path = FileExtension
                .GetFilePath(_environment.WebRootPath, "assets/images", blogImage.Name);

            FileExtension.DeleteFile(path);

            _context.BlogImages.Remove(blogImage);
            await _context.SaveChangesAsync();
        }

        public async Task<ICollection<BlogImage>> GetAllByBlogId(int blogId)
        {
            return await _context.BlogImages.Where(m => m.BlogId == blogId).ToListAsync();
        }

        public async Task<BlogImage> GetByIdAsync(int id)
        {
            return await _context.BlogImages.Include(m => m.Blog)
                .ThenInclude(m => m.BlogImages).FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task UpdateType(int id)
        {
            var existItem = await GetByIdAsync(id);

            foreach (var item in existItem.Blog.BlogImages) item.IsMain = false;

            existItem.IsMain = true;

            await _context.SaveChangesAsync();
        }
    }
}

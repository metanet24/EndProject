using AutoMapper;
using MBEAUTY.Data;
using MBEAUTY.Models;
using MBEAUTY.Services.Interfaces;
using MBEAUTY.ViewModels.BlogVMs;
using Microsoft.EntityFrameworkCore;

namespace MBEAUTY.Services.Implementations
{
    public class BlogService : IBlogService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public BlogService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> AddAsync(BlogAddVM item)
        {
            var newItem = _mapper.Map<Blog>(item);

            await _context.Blogs.AddAsync(newItem);
            await _context.SaveChangesAsync();

            return newItem.Id;
        }

        public async Task UpdateAsync(BlogEditVM item)
        {
            var existItem = await GetByIdAsync(item.Id);

            _context.Blogs.Update(_mapper.Map(item, existItem));
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Blog blog)
        {
            _context.Blogs.Remove(blog);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Blog>> GetAllAsync()
        {
            return await _context.Blogs
                .Where(m => !m.SoftDeleted)
                .Include(m => m.BlogImages)
                .OrderByDescending(m => m.Id)
                .ToListAsync();
        }

        public async Task<Blog> GetByIdAsync(int id)
        {
            return await _context.Blogs
                .Where(m => !m.SoftDeleted)
                .Include(m => m.BlogImages)
                .OrderByDescending(m => m.Id)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<int> GetPageCount(int take)
        {
            IEnumerable<Blog> blogs = await GetAllAsync();

            return (int)Math.Ceiling((decimal)blogs.Count() / take);
        }
    }
}

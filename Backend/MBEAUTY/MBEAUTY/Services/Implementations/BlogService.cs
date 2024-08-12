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

        public Task AddAsync(Blog blog)
        {
            throw new NotImplementedException();
        }

        public void Delete(Blog blog)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<BlogListVM>> GetAllAsync()
        {
            IEnumerable<Blog> blogs = await _context.Blogs
                .Where(m => !m.SoftDeleted)
                .Include(m => m.BlogImages)
                .OrderByDescending(m => m.Id)
                .ToListAsync();

            return _mapper.Map<IEnumerable<BlogListVM>>(blogs);
        }

        public Task<Blog> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task SaveAsync()
        {
            throw new NotImplementedException();
        }
    }
}

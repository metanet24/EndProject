using AutoMapper;
using MBEAUTY.Data;
using MBEAUTY.Models;
using MBEAUTY.Services.Interfaces;
using MBEAUTY.ViewModels.CategoryVMs;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace MBEAUTY.Services.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public CategoryService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task AddAsync(CategoryAddVM category)
        {
            await _context.Categories.AddAsync(_mapper.Map<Category>(category));
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(CategoryEditVM item)
        {
            var existItem = await GetByIdAsync(item.Id);
            _context.Categories.Update(_mapper.Map(item, existItem));
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Category category)
        {
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _context.Categories.Where(m => !m.SoftDeleted)
                .Include(m => m.Products).OrderByDescending(m => m.Id).ToListAsync();
        }

        public async Task<SelectList> GetAllSelectAsync()
        {
            return new SelectList(await GetAllAsync(), "Id", "Name");
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            return await _context.Categories.Where(m => !m.SoftDeleted).FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<int> GetPageCount(int take)
        {
            IEnumerable<Category> items = await GetAllAsync();

            return (int)Math.Ceiling((decimal)items.Count() / take);
        }
    }
}

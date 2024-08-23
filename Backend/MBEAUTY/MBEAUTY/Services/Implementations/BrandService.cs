using AutoMapper;
using MBEAUTY.Data;
using MBEAUTY.Models;
using MBEAUTY.Services.Interfaces;
using MBEAUTY.ViewModels.BrandVMs;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace MBEAUTY.Services.Implementations
{
    public class BrandService : IBrandService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public BrandService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task AddAsync(BrandAddVM item)
        {
            var newItem = _mapper.Map<Brand>(item);

            await _context.Brands.AddAsync(newItem);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Brand item)
        {
            var existItem = await GetByIdAsync(item.Id);

            _context.Brands.Update(_mapper.Map(item, existItem));
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Brand>> GetAllAsync()
        {
            return await _context.Brands.Where(m => !m.SoftDeleted)
                .Include(m => m.Products).OrderByDescending(m => m.Id).ToListAsync();
        }

        public async Task<SelectList> GetAllSelectAsync()
        {
            return new SelectList(await GetAllAsync(), "Id", "Name");
        }

        public async Task<Brand> GetByIdAsync(int id)
        {
            return await _context.Brands.Where(m => !m.SoftDeleted).FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<int> GetPageCount(int take)
        {
            IEnumerable<Brand> brands = await GetAllAsync();

            return (int)Math.Ceiling((decimal)brands.Count() / take);
        }
    }
}

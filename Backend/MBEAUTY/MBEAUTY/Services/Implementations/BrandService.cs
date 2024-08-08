using AutoMapper;
using MBEAUTY.Data;
using MBEAUTY.Models;
using MBEAUTY.Services.Interfaces;
using MBEAUTY.ViewModels.BrandVMs;
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

        public Task AddAsync(Brand product)
        {
            throw new NotImplementedException();
        }

        public void Delete(Brand product)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<BrandListVM>> GetAllAsync()
        {
            IEnumerable<Brand> brands = await _context.Brands.Where(m => !m.SoftDeleted)
                .OrderByDescending(m => m.Id).ToListAsync();

            return _mapper.Map<IEnumerable<BrandListVM>>(brands);
        }

        public Task<Product> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task SaveAsync()
        {
            throw new NotImplementedException();
        }
    }
}

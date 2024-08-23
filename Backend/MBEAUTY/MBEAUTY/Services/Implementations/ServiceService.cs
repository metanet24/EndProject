using AutoMapper;
using MBEAUTY.Data;
using MBEAUTY.Models;
using MBEAUTY.Services.Interfaces;
using MBEAUTY.ViewModels.ServicesVMs;
using Microsoft.EntityFrameworkCore;

namespace MBEAUTY.Services.Implementations
{
    public class ServiceService : IServiceService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ServiceService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task AddAsync(ServiceAddVM item)
        {
            await _context.Services.AddAsync(_mapper.Map<Service>(item));
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ServiceEditVM item)
        {
            var existItem = await GetByIdAsync(item.Id);
            _context.Services.Update(_mapper.Map(item, existItem));
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Service item)
        {
            _context.Services.Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Service>> GetAllAsync()
        {
            return await _context.Services.Where(m => !m.SoftDeleted)
                .OrderByDescending(m => m.Id).ToListAsync();
        }

        public async Task<Service> GetByIdAsync(int id)
        {
            return await _context.Services.Where(m => !m.SoftDeleted).FirstOrDefaultAsync(m => m.Id == id);
        }
    }
}

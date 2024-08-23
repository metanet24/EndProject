using AutoMapper;
using MBEAUTY.Data;
using MBEAUTY.Models;
using MBEAUTY.Services.Interfaces;
using MBEAUTY.ViewModels.SettingVMs;
using Microsoft.EntityFrameworkCore;

namespace MBEAUTY.Services.Implementations
{
    public class SettingService : ISettingService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public SettingService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task AddAsync(SettingAddVM item)
        {
            await _context.Settings.AddAsync(_mapper.Map<Setting>(item));
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(SettingEditVM item)
        {
            var existItem = await GetByIdAsync(item.Id);
            _context.Settings.Update(_mapper.Map(item, existItem));
            await _context.SaveChangesAsync();
        }

        public async Task<Setting> GetByIdAsync(int id)
        {
            return await _context.Settings.Where(m => !m.SoftDeleted).FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<IEnumerable<Setting>> GetAllAsync()
        {
            return await _context.Settings.Where(m => !m.SoftDeleted).OrderByDescending(m => m.Id).ToListAsync();
        }

        public async Task DeleteAsync(Setting item)
        {
            _context.Settings.Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task<Dictionary<string, string>> GetAllDictionaryAsync()
        {
            return await _context.Settings
                .Where(m => !m.SoftDeleted)
                .ToDictionaryAsync(m => m.Key, m => m.Value);
        }
    }
}

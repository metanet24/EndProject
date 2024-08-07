using MBEAUTY.Data;
using MBEAUTY.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MBEAUTY.Services.Implementations
{
    public class SettingService : ISettingService
    {
        private readonly AppDbContext _context;

        public SettingService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Dictionary<string, string>> GetAllAsync()
        {
            return await _context.Settings
                .Where(m => !m.SoftDeleted)
                .ToDictionaryAsync(m => m.Key, m => m.Value);
        }
    }
}

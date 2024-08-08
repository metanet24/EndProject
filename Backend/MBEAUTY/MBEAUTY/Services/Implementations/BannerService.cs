using AutoMapper;
using MBEAUTY.Data;
using MBEAUTY.Models;
using MBEAUTY.Services.Interfaces;
using MBEAUTY.ViewModels.BannnerVMs;
using Microsoft.EntityFrameworkCore;

namespace MBEAUTY.Services.Implementations
{
    public class BannerService : IBannerService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public BannerService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<BannerVM> GetAsync()
        {
            Banner banner = await _context.Banners.Where(m => !m.SoftDeleted).FirstOrDefaultAsync();

            return _mapper.Map<BannerVM>(banner);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Banner model)
        {
            _context.Banners.Update(model);
        }
    }
}

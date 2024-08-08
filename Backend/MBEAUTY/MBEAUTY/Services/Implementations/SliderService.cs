using AutoMapper;
using MBEAUTY.Data;
using MBEAUTY.Models;
using MBEAUTY.Services.Interfaces;
using MBEAUTY.ViewModels.SliderVMs;
using Microsoft.EntityFrameworkCore;

namespace MBEAUTY.Services.Implementations
{
    public class SliderService : ISliderService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public SliderService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task AddAsync(Slider slider)
        {
            await _context.Sliders.AddAsync(slider);
        }

        public void Delete(Slider slider)
        {
            _context.Sliders.Remove(slider);
        }

        public async Task<IEnumerable<SliderListVM>> GetAllAsync()
        {
            IEnumerable<Slider> sliders = await _context.Sliders.Where(m => !m.SoftDeleted)
                .OrderByDescending(m => m.Id).ToListAsync();

            return _mapper.Map<IEnumerable<SliderListVM>>(sliders);
        }

        public async Task<Slider> GetByIdAsync(int id)
        {
            return await _context.Sliders.FirstOrDefaultAsync(m => !m.SoftDeleted && m.Id == id);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}

using AutoMapper;
using Fruitables_Backend.Helpers;
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
        private readonly IWebHostEnvironment _environment;
        private readonly IMapper _mapper;

        public SliderService(AppDbContext context, IMapper mapper, IWebHostEnvironment environment)
        {
            _context = context;
            _mapper = mapper;
            _environment = environment;
        }

        public async Task AddAsync(SliderAddVM item)
        {
            string fileName = Guid.NewGuid().ToString() + "_" + item.Photo.FileName;

            string path = FileExtension.GetFilePath(_environment.WebRootPath, "assets/images", fileName);
            await FileExtension.SaveFile(path, item.Photo);

            var newItem = _mapper.Map<Slider>(item);
            newItem.Image = fileName;

            await _context.Sliders.AddAsync(newItem);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(SliderEditVM item)
        {
            var existItem = await GetByIdAsync(item.Id);

            if (item.Photo != null)
            {
                string fileName = Guid.NewGuid().ToString() + "_" + item.Photo.FileName;

                string path = FileExtension.GetFilePath(_environment.WebRootPath, "assets/images", fileName);
                await FileExtension.SaveFile(path, item.Photo);

                string oldPath = FileExtension
                    .GetFilePath(_environment.WebRootPath, "assets/images", existItem.Image);

                FileExtension.DeleteFile(oldPath);

                item.Image = fileName;
            }

            _context.Sliders.Update(_mapper.Map(item, existItem));
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Slider item)
        {
            string oldPath = FileExtension.GetFilePath(_environment.WebRootPath, "assets/images", item.Image);

            FileExtension.DeleteFile(oldPath);

            _context.Sliders.Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Slider>> GetAllAsync()
        {
            return await _context.Sliders.Where(m => !m.SoftDeleted)
                .OrderByDescending(m => m.Id).ToListAsync();
        }

        public async Task<Slider> GetByIdAsync(int id)
        {
            return await _context.Sliders.FirstOrDefaultAsync(m => !m.SoftDeleted && m.Id == id);
        }
    }
}

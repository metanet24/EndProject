using AutoMapper;
using Fruitables_Backend.Helpers;
using MBEAUTY.Data;
using MBEAUTY.Models;
using MBEAUTY.Services.Interfaces;
using MBEAUTY.ViewModels.AboutVms;
using Microsoft.EntityFrameworkCore;

namespace MBEAUTY.Services.Implementations
{
    public class AboutService : IAboutService
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _environment;
        private readonly IMapper _mapper;

        public AboutService(AppDbContext context, IMapper mapper, IWebHostEnvironment environment)
        {
            _context = context;
            _mapper = mapper;
            _environment = environment;
        }

        public async Task<About> GetAsync()
        {
            return await _context.Abouts.Where(m => !m.SoftDeleted).FirstOrDefaultAsync();
        }

        public async Task<About> GetByIdAsync(int id)
        {
            return await _context.Abouts.Where(m => !m.SoftDeleted).FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task UpdateAsync(AboutEditVM model)
        {
            var existItem = await GetByIdAsync(model.Id);

            if (model.Photo != null)
            {
                string fileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;

                string path = FileExtension.GetFilePath(_environment.WebRootPath, "assets/images", fileName);
                await FileExtension.SaveFile(path, model.Photo);

                string oldPath = FileExtension
                    .GetFilePath(_environment.WebRootPath, "assets/images", existItem.Image);

                FileExtension.DeleteFile(oldPath);

                model.Image = fileName;
            }

            _context.Abouts.Update(_mapper.Map(model, existItem));
            await _context.SaveChangesAsync();
        }
    }
}

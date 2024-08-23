using AutoMapper;
using Fruitables_Backend.Helpers;
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
        private readonly IWebHostEnvironment _environment;
        private readonly IMapper _mapper;

        public BannerService(AppDbContext context, IMapper mapper, IWebHostEnvironment environment)
        {
            _context = context;
            _mapper = mapper;
            _environment = environment;
        }

        public async Task<Banner> GetAsync()
        {
            return await _context.Banners.Where(m => !m.SoftDeleted).FirstOrDefaultAsync();
        }

        public async Task<Banner> GetByIdAsync(int id)
        {
            return await _context.Banners.Where(m => !m.SoftDeleted).FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task UpdateAsync(BannerEditVM model)
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

            _context.Banners.Update(_mapper.Map(model, existItem));
            await _context.SaveChangesAsync();
        }
    }
}

using AutoMapper;
using Fruitables_Backend.Helpers;
using MBEAUTY.Data;
using MBEAUTY.Models;
using MBEAUTY.Services.Interfaces;
using MBEAUTY.ViewModels.AdvertVMs;
using Microsoft.EntityFrameworkCore;

namespace MBEAUTY.Services.Implementations
{
    public class AdvertService : IAdvertService
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _environment;
        private readonly IMapper _mapper;

        public AdvertService(AppDbContext context, IMapper mapper, IWebHostEnvironment environment)
        {
            _context = context;
            _mapper = mapper;
            _environment = environment;
        }

        public async Task AddAsync(AdvertAddVM item)
        {
            string fileName = Guid.NewGuid().ToString() + "_" + item.Photo.FileName;

            string path = FileExtension.GetFilePath(_environment.WebRootPath, "assets/images", fileName);
            await FileExtension.SaveFile(path, item.Photo);

            var newItem = _mapper.Map<Advert>(item);
            newItem.Image = fileName;

            await _context.Adverts.AddAsync(newItem);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(AdvertEditVM item)
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

            _context.Adverts.Update(_mapper.Map(item, existItem));
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Advert item)
        {
            string oldPath = FileExtension.GetFilePath(_environment.WebRootPath, "assets/images", item.Image);

            FileExtension.DeleteFile(oldPath);

            _context.Adverts.Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task<Advert> GetAsync()
        {
            return await _context.Adverts.Where(m => !m.SoftDeleted).FirstOrDefaultAsync();
        }

        public async Task<Advert> GetByIdAsync(int id)
        {
            return await _context.Adverts.Where(m => !m.SoftDeleted).FirstOrDefaultAsync(m => m.Id == id);
        }
    }
}

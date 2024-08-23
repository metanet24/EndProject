using AutoMapper;
using Fruitables_Backend.Helpers;
using MBEAUTY.Data;
using MBEAUTY.Models;
using MBEAUTY.Services.Interfaces;
using MBEAUTY.ViewModels.FamousVms;
using Microsoft.EntityFrameworkCore;

namespace MBEAUTY.Services.Implementations
{
    public class FamousService : IFamousService
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _environment;
        private readonly IMapper _mapper;

        public FamousService(AppDbContext context, IMapper mapper, IWebHostEnvironment environment)
        {
            _context = context;
            _mapper = mapper;
            _environment = environment;
        }

        public async Task AddAsync(FamousAddVM item)
        {
            string fileName = Guid.NewGuid().ToString() + "_" + item.Photo.FileName;

            string path = FileExtension.GetFilePath(_environment.WebRootPath, "assets/images", fileName);
            await FileExtension.SaveFile(path, item.Photo);

            var newItem = _mapper.Map<Famous>(item);
            newItem.Image = fileName;

            await _context.Famous.AddAsync(newItem);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(FamousEditVM item)
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

            _context.Famous.Update(_mapper.Map(item, existItem));
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Famous item)
        {
            string oldPath = FileExtension.GetFilePath(_environment.WebRootPath, "assets/images", item.Image);
            FileExtension.DeleteFile(oldPath);

            _context.Famous.Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Famous>> GetAllAsync()
        {
            return await _context.Famous.Where(m => !m.SoftDeleted)
                .OrderByDescending(m => m.Id).ToListAsync();
        }

        public async Task<Famous> GetByIdAsync(int id)
        {
            return await _context.Famous.Where(m => !m.SoftDeleted).FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<int> GetPageCount(int take)
        {
            IEnumerable<Famous> items = await GetAllAsync();

            return (int)Math.Ceiling((decimal)items.Count() / take);
        }
    }
}

using AutoMapper;
using Fruitables_Backend.Helpers;
using MBEAUTY.Data;
using MBEAUTY.Models;
using MBEAUTY.Services.Interfaces;
using MBEAUTY.ViewModels.BrandVMs;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace MBEAUTY.Services.Implementations
{
    public class BrandService : IBrandService
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _environment;
        private readonly IMapper _mapper;

        public BrandService(AppDbContext context, IMapper mapper, IWebHostEnvironment environment)
        {
            _context = context;
            _mapper = mapper;
            _environment = environment;
        }

        public async Task AddAsync(BrandAddVM item)
        {
            string fileName = Guid.NewGuid().ToString() + "_" + item.Photo.FileName;

            string path = FileExtension.GetFilePath(_environment.WebRootPath, "assets/images", fileName);
            await FileExtension.SaveFile(path, item.Photo);

            var newItem = _mapper.Map<Brand>(item);
            newItem.Logo = fileName;

            await _context.Brands.AddAsync(newItem);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(BrandEditVM item)
        {
            var existItem = await GetByIdAsync(item.Id);

            if (item.Photo != null)
            {
                string fileName = Guid.NewGuid().ToString() + "_" + item.Photo.FileName;

                string path = FileExtension.GetFilePath(_environment.WebRootPath, "assets/images", fileName);
                await FileExtension.SaveFile(path, item.Photo);

                string oldPath = FileExtension
                    .GetFilePath(_environment.WebRootPath, "assets/images", existItem.Logo);

                FileExtension.DeleteFile(oldPath);

                item.Logo = fileName;
            }

            _context.Brands.Update(_mapper.Map(item, existItem));
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Brand item)
        {
            string oldPath = FileExtension.GetFilePath(_environment.WebRootPath, "assets/images", item.Logo);

            FileExtension.DeleteFile(oldPath);

            _context.Brands.Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Brand>> GetAllAsync()
        {
            return await _context.Brands.Where(m => !m.SoftDeleted)
                .Include(m => m.Products).OrderByDescending(m => m.Id).ToListAsync();
        }

        public async Task<SelectList> GetAllSelectAsync()
        {
            return new SelectList(await GetAllAsync(), "Id", "Name");
        }

        public async Task<Brand> GetByIdAsync(int id)
        {
            return await _context.Brands.Where(m => !m.SoftDeleted).FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<int> GetPageCount(int take)
        {
            IEnumerable<Brand> brands = await GetAllAsync();

            return (int)Math.Ceiling((decimal)brands.Count() / take);
        }
    }
}

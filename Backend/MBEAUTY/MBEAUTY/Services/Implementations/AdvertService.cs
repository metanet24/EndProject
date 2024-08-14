using AutoMapper;
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
        private readonly IMapper _mapper;

        public AdvertService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public Task AddAsync(Advert advert)
        {
            throw new NotImplementedException();
        }

        public void Delete(Advert advert)
        {
            throw new NotImplementedException();
        }

        public async Task<AdvertVM> GetAsync()
        {
            Advert advert = await _context.Adverts.Where(m => !m.SoftDeleted).FirstOrDefaultAsync();

            return _mapper.Map<AdvertVM>(advert);
        }

        public Task<Advert> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task SaveAsync()
        {
            throw new NotImplementedException();
        }
    }
}

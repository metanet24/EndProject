using AutoMapper;
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
        private readonly IMapper _mapper;

        public FamousService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public Task AddAsync(Famous famous)
        {
            throw new NotImplementedException();
        }

        public void Delete(Famous famous)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<FamousListVM>> GetAllAsync()
        {
            IEnumerable<Famous> famous = await _context.Famous.Where(m => !m.SoftDeleted)
                .OrderByDescending(m => m.Id).ToListAsync();

            return _mapper.Map<IEnumerable<FamousListVM>>(famous);
        }

        public Task<Famous> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task SaveAsync()
        {
            throw new NotImplementedException();
        }
    }
}

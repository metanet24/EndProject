using AutoMapper;
using MBEAUTY.Data;
using MBEAUTY.Models;
using MBEAUTY.Services.Interfaces;
using MBEAUTY.ViewModels.ServicesVMs;
using Microsoft.EntityFrameworkCore;

namespace MBEAUTY.Services.Implementations
{
    public class ServiceService : IServiceService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ServiceService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public Task AddAsync(Service service)
        {
            throw new NotImplementedException();
        }

        public void Delete(Service service)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ServiceListVM>> GetAllAsync()
        {
            IEnumerable<Service> services = await _context.Services.Where(m => !m.SoftDeleted)
                .OrderByDescending(m => m.Id).ToListAsync();

            return _mapper.Map<IEnumerable<ServiceListVM>>(services);
        }

        public Task<Service> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task SaveAsync()
        {
            throw new NotImplementedException();
        }
    }
}

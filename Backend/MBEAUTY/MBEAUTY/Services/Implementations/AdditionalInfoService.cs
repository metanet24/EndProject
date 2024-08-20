using AutoMapper;
using MBEAUTY.Data;
using MBEAUTY.Models;
using MBEAUTY.Services.Interfaces;
using MBEAUTY.ViewModels.AdditionalInfoVMs;

namespace MBEAUTY.Services.Implementations
{
    public class AdditionalInfoService : IAdditionalInfoService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public AdditionalInfoService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task AddAsync(AdditionalInfoAddVM additionalInfo)
        {
            await _context.AdditionalInfos.AddAsync(_mapper.Map<AdditionalInfo>(additionalInfo));
            await _context.SaveChangesAsync();
        }
    }
}

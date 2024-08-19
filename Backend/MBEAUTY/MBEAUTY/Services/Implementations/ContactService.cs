using AutoMapper;
using MBEAUTY.Data;
using MBEAUTY.Models;
using MBEAUTY.Services.Interfaces;
using MBEAUTY.ViewModels.ContactVMs;

namespace MBEAUTY.Services.Implementations
{
    public class ContactService : IContactService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ContactService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task AddAsync(ContactAddVM model)
        {
            await _context.Contacts.AddAsync(_mapper.Map<Contact>(model));
            await _context.SaveChangesAsync();
        }
    }
}

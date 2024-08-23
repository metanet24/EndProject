using AutoMapper;
using MBEAUTY.Data;
using MBEAUTY.Models;
using MBEAUTY.Services.Interfaces;
using MBEAUTY.ViewModels.ContactVMs;
using Microsoft.EntityFrameworkCore;

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

        public async Task DeleteAsync(Contact model)
        {
            _context.Contacts.Remove(model);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Contact>> GetAllAsync()
        {
            return await _context.Contacts.Where(m => !m.SoftDeleted).ToListAsync();
        }

        public async Task<Contact> GetByIdAsync(int id)
        {
            return await _context.Contacts.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<int> GetPageCount(int take)
        {
            IEnumerable<Contact> items = await GetAllAsync();

            return (int)Math.Ceiling((decimal)items.Count() / take);
        }
    }
}

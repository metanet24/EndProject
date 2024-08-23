using MBEAUTY.Models;
using MBEAUTY.ViewModels.ContactVMs;

namespace MBEAUTY.Services.Interfaces
{
    public interface IContactService
    {
        Task AddAsync(ContactAddVM model);
        Task DeleteAsync(Contact model);
        Task<IEnumerable<Contact>> GetAllAsync();
        Task<Contact> GetByIdAsync(int id);
        Task<int> GetPageCount(int take);
    }
}

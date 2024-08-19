using MBEAUTY.ViewModels.ContactVMs;

namespace MBEAUTY.Services.Interfaces
{
    public interface IContactService
    {
        Task AddAsync(ContactAddVM model);
    }
}

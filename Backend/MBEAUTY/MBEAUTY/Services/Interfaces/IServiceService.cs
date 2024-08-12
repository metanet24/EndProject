using MBEAUTY.Models;
using MBEAUTY.ViewModels.ServicesVMs;

namespace MBEAUTY.Services.Interfaces
{
    public interface IServiceService
    {
        Task AddAsync(Service service);
        void Delete(Service service);
        Task<IEnumerable<ServiceListVM>> GetAllAsync();
        Task<Service> GetByIdAsync(int id);
        Task SaveAsync();
    }
}

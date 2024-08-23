using MBEAUTY.Models;
using MBEAUTY.ViewModels.ServicesVMs;

namespace MBEAUTY.Services.Interfaces
{
    public interface IServiceService
    {
        Task AddAsync(ServiceAddVM item);
        Task UpdateAsync(ServiceEditVM item);
        Task DeleteAsync(Service item);
        Task<IEnumerable<Service>> GetAllAsync();
        Task<Service> GetByIdAsync(int id);
    }
}

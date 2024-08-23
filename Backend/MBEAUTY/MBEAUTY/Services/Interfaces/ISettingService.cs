using MBEAUTY.Models;
using MBEAUTY.ViewModels.SettingVMs;

namespace MBEAUTY.Services.Interfaces
{
    public interface ISettingService
    {
        Task AddAsync(SettingAddVM item);
        Task UpdateAsync(SettingEditVM item);
        Task<Setting> GetByIdAsync(int id);
        Task<IEnumerable<Setting>> GetAllAsync();
        Task DeleteAsync(Setting item);
        Task<Dictionary<string, string>> GetAllDictionaryAsync();
    }
}

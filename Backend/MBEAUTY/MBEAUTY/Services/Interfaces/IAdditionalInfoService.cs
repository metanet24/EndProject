using MBEAUTY.Models;
using MBEAUTY.ViewModels.AdditionalInfoVMs;

namespace MBEAUTY.Services.Interfaces
{
    public interface IAdditionalInfoService
    {
        Task AddAsync(AdditionalInfoAddVM additionalInfo);
        Task<AdditionalInfo> GetByProductIdAsync(int productId);
        Task UpdateAsync(AdditionalInfoEditVM additionalInfo);
    }
}

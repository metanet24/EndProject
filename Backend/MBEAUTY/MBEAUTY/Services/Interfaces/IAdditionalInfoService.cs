using MBEAUTY.ViewModels.AdditionalInfoVMs;

namespace MBEAUTY.Services.Interfaces
{
    public interface IAdditionalInfoService
    {
        Task AddAsync(AdditionalInfoAddVM additionalInfo);
    }
}

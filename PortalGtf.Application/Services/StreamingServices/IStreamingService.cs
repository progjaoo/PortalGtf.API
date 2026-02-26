using PortalGtf.Application.ViewModels.StreamingVM;

namespace PortalGtf.Application.Services.StreamingServices;

public interface IStreamingService
{
    Task<List<StreamingViewModel>> GetAllAsync();
    Task<StreamingViewModel?> GetByIdAsync(int id);
    Task<int> CreateAsync(StreamingCreateViewModel model);
    Task<bool> UpdateAsync(int id, StreamingCreateViewModel model);
    Task<bool> DeleteAsync(int id);
}
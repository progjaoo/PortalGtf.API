using PortalGtf.Application.ViewModels.RegiaoVM;

namespace PortalGtf.Application.Services.RegiaoServices;

public interface IRegiaoService
{
    Task<List<RegiaoViewModel>> GetAllAsync();
    Task<RegiaoViewModel?> GetByIdAsync(int id);
    Task<int> CreateAsync(RegiaoCreateViewModel model);
    Task<bool> UpdateAsync(int id, RegiaoCreateViewModel model);
    Task<bool> DeleteAsync(int id);
}
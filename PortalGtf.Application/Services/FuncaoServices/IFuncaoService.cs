using PortalGtf.Application.ViewModels.FuncaoVM;

namespace PortalGtf.Application.Services.FuncaoServices;

public interface IFuncaoService
{
    Task<List<FuncaoViewModel>> GetAllAsync();
    Task<FuncaoViewModel?> GetByIdAsync(int id);
    Task<int> CreateAsync(FuncaoCreateViewModel model);
    Task<bool> UpdateAsync(int id, FuncaoViewModel model);
    Task<bool> DeleteAsync(int id);
}
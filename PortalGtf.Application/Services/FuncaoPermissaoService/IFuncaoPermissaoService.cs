using PortalGtf.Application.ViewModels.FuncaoPermissaoVM;

namespace PortalGtf.Application.Services.FuncaoPermissaoService;

public interface IFuncaoPermissaoService
{
    Task<List<FuncaoPermissaoViewModel>> GetAllAsync();
    Task<int> CreateAsync(FuncaoPermissaoCreateViewModel model);
    Task<bool> DeleteAsync(int id);
}
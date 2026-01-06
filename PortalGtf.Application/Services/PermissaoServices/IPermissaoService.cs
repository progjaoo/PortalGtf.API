using PortalGtf.Application.ViewModels.PermissaoVM;

namespace PortalGtf.Application.Services.PermissaoServices;

public interface IPermissaoService
{
    Task<List<PermissaoResponseViewModel>> GetAllPermissoesByUsuarioAsync(int usuarioId);
    Task<List<PermissaoViewmModel>> GetAllAsync();
    Task<PermissaoViewmModel?> GetByIdAsync(int id);
    Task<int> CreateAsync(PermissaoCreateViewModel model);
    Task<bool> UpdateAsync(int id, PermissaoCreateViewModel model);
    Task<bool> DeleteAsync(int id);
}
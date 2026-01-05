using PortalGtf.Application.ViewModels.UsuarioVM;

namespace PortalGtf.Application.Services.UsuarioServices;

public interface IUsuarioService
{
    Task<IEnumerable<UsuarioResponseViewModel>> GetAllAsync();
    Task<UsuarioResponseViewModel?> GetByIdAsync(int id);
    Task CreateAsync(UsuarioCreateViewModel model);
}
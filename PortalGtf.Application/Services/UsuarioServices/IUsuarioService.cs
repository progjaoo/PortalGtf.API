using PortalGtf.Application.ViewModels.LoginVM;
using PortalGtf.Application.ViewModels.UsuarioVM;
using PortalGtf.Core.Entities;

namespace PortalGtf.Application.Services.UsuarioServices;

public interface IUsuarioService
{
    Task<IEnumerable<UsuarioResponseViewModel>> GetAllAsync();
    Task<UsuarioResponseViewModel?> GetByIdAsync(int id);
    Task CreateAsync(UsuarioCreateViewModel model);
    Task<LoginResponseViewModel?> LoginUserAsync(LoginRequestViewModel model);
    Task AtivarUsuarioAsync(int usuarioId);
    Task DesativarUsuarioAsync(int usuarioId);
    Task UpdateAsync(int id, UsuarioUpdateViewModel model);
}
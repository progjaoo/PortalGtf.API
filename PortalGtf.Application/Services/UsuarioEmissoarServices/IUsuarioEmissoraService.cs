using PortalGtf.Application.Services.UsuarioEmissoraService;

namespace PortalGtf.Application.Services.UsuarioEmissoarServices;

public interface IUsuarioEmissoraService
{
    Task<List<UsuarioEmissoraViewModel>> GetAllAsync();
    Task<int> CreateAsync(UsuarioEmissoraCreateViewModel model);
    Task<bool> DeleteAsync(int id);
}
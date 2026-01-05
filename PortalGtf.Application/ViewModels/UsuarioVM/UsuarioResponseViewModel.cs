using PortalGtf.Core.Enums;

namespace PortalGtf.Application.ViewModels.UsuarioVM;

public class UsuarioResponseViewModel
{
    public int Id { get; set; }
    public string Email { get; set; } = null!;
    public string NomeCompleto { get; set; } = null!;
    public StatusUsuario? StatusUsuario { get; set; } = null!;
}
namespace PortalGtf.Application.ViewModels.PermissaoVM;

public class PermissaoUsuarioResponseViewModel
{
    public int UsuarioId { get; set; }
    public List<PermissaoResponseViewModel> Permissoes { get; set; } = new();
}
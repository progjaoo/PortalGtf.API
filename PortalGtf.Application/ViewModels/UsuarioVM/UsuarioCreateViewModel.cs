namespace PortalGtf.Application.ViewModels.UsuarioVM;

public class UsuarioCreateViewModel
{
    public string Email { get; set; } = null!;
    public string NomeCompleto { get; set; } = null!;
    public string Senha { get; set; } = null!;
    public int FuncaoId { get; set; }
}
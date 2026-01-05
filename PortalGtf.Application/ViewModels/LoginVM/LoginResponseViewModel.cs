namespace PortalGtf.Application.ViewModels.LoginVM;

public class LoginResponseViewModel
{
    public int UsuarioId { get; set; }
    public string Email { get; set; } = null!;
    public string Token { get; set; } = null!;
    public string Funcao { get; set; } = null!;
}
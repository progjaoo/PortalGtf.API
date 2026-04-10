namespace PortalGtf.Application.Services.UsuarioEmissoraService;

public class UsuarioEmissoraViewModel
{
    public int Id { get; set; }
    public int UsuarioId { get; set; }
    public int EmissoraId { get; set; }
    public int FuncaoId { get; set; }
    public string? UsuarioNome { get; set; }
    public string? EmissoraNome { get; set; }
    public string? FuncaoNome { get; set; }
}

namespace PortalGtf.Core.Entities;

public class Notificacao
{
    public int Id { get; set; }
    public int UsuarioId { get; set; }
    public Usuario Usuario { get; set; } = null!;
    public string Titulo { get; set; } = null!;
    public string Mensagem { get; set; } = null!;
    public bool Lida { get; set; }
    public DateTime DataCriacao { get; set; }
}
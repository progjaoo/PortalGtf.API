namespace PortalGtf.Core.Entities;

public class PostHistorico
{
    public int Id { get; set; }
    public int PostId { get; set; }
    public Post Post { get; set; } = null!;

    public int UsuarioId { get; set; }
    public Usuario Usuario { get; set; } = null!;

    public string Acao { get; set; } = null!;
    public DateTime DataAcao { get; set; }
}
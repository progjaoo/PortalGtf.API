using PortalGtf.Core.Enums;

namespace PortalGtf.Core.Entities;

public class PostImagem
{
    public int Id { get; set; }
    public int PostId { get; set; }
    public Post Post { get; set; } = null!;
    public int MidiaId { get; set; }
    public Midia Midia { get; set; } = null!;
    public int Ordem { get; set; }
    public TipoMidia Tipo { get; set; }
}
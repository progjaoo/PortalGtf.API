namespace PortalGtf.Core.Entities;

public class PostComentario
{
    public int Id { get; set; }
    public int PostId { get; set; }
    public Post Post { get; set; } = null!;

    public int ComentarioId { get; set; }
    public Comentario Comentario { get; set; } = null!;
}
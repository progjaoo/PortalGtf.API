namespace PortalGtf.Core.Entities;

public class Comentario
{
    public int Id { get; set; }
    public int PostId { get; set; }
    public Post Post { get; set; } = null!;

    public int? UsuarioId { get; set; }
    public Usuario? Usuario { get; set; }

    public string? NomeVisitante { get; set; }
    public string Conteudo { get; set; } = null!;
    public string Status { get; set; } = "Pendente";
    public DateTime DataCriacao { get; set; }

    public ICollection<PostComentario> PostComentarios { get; set; } = new List<PostComentario>();
}
using PortalGtf.Core.Enums;

namespace PortalGtf.Core.Entities;

public class Post
{
    public int Id { get; set; }
    public string Titulo { get; set; } = null!;
    public string Subtitulo { get; set; } = null!;
    public string Conteudo { get; set; } = null!;
    public string Imagem { get; set; } = null!;
    public string Slug { get; set; } = null!;
    public int EditorialId { get; set; }
    public Editorial Editorial { get; set; } = null!;
    public int EmissoraId { get; set; }
    public Emissora Emissora { get; set; } = null!;
    public int CidadeId { get; set; }
    public Cidade Cidade { get; set; } = null!;
    public int UsuarioCriacaoId { get; set; }
    public Usuario UsuarioCriacao { get; set; } = null!;
    public int UsuarioAprovacaoId { get; set; }
    public Usuario UsuarioAprovacao { get; set; } = null!;
    public StatusPost StatusPost { get; set; }
    public DateTime DataCriacao { get; set; }
    public DateTime? DataAprovacao { get; set; }
    public DateTime? DataEdicao { get; set; }
    
    public DateTime? PublicadoEm { get; set; }
    public ICollection<PostImagem> Imagens { get; set; } = new List<PostImagem>();
    public ICollection<PostTag> PostTags { get; set; } = new List<PostTag>();
    public ICollection<PostVisualizacao> Visualizacoes { get; set; } = new List<PostVisualizacao>();
    public ICollection<PostComentario> PostComentarios { get; set; } = new List<PostComentario>();
    public ICollection<Comentario> Comentarios { get; set; } = new List<Comentario>();
}
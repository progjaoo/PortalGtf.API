namespace PortalGtf.Application.ViewModels.PostsVM;

public class PostPublicViewModel
{
    public int Id { get; set; }
    public string Titulo { get; set; } = null!;
    public string Conteudo { get; set; } = null!;
    public string Subtitulo { get; set; } = null!;
    public string? ImagemCapaUrl { get; set; }
    public int? ImagemCapaId { get; set; }

    public string Slug { get; set; } = null!;
    public DateTime? PublicadoEm { get; set; }
    public string Editorial { get; set; } = null!;
    public string CorTema { get; set; } = null!;
    public int UsuarioCriacaoId { get; set; }
    public string UsuarioCriacao { get; set; }
    public string Emissora { get; set; } = null!;
    public string? EmissoraSlug { get; set; }
    
    public string Cidade { get; set; } = null!;

    public int TotalVisualizacoes { get; set; }
    
    public List<PostImagemViewModel> Midias { get; set; } = new();

}

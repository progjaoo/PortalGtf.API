namespace PortalGtf.Application.ViewModels.PostsVM;

public class PostResumeViewModel
{
    public int Id { get; set; }
    public string Titulo { get; set; } = null!;
    public string? Subtitulo { get; set; }
    public string Slug { get; set; } = null!;
    public string? Imagem { get; set; }
    public DateTime? PublicadoEm { get; set; }

    public string Editorial { get; set; } = null!;
    public string CorTema { get; set; } = null!;
    public int UsuarioCriacaoId { get; set; }
    public string UsuarioCriacao { get; set; } = null!;
    public string Emissora { get; set; } = null!;
    public string Cidade { get; set; } = null!;

    public string EmissoraNome { get; set; } = null!;
    public string EmissoraSlug { get; set; } = null!;
    public string EmissoraLogo { get; set; } = null!;
}
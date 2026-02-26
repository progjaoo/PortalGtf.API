namespace PortalGtf.Application.ViewModels.PostsVM;

public class PostPublicViewModel
{
    public int Id { get; set; }
    public string Titulo { get; set; } = null!;
    public string Conteudo { get; set; } = null!;
    public string Subtitulo { get; set; } = null!;
    public string Slug { get; set; } = null!;
    public string Imagem { get; set; } = null!;
    public DateTime? PublicadoEm { get; set; }
    public string Editorial { get; set; } = null!;
    public string CorTema { get; set; } = null!;
    public int UsuarioCriacaoId { get; set; }
    public string UsuarioCriacao { get; set; }
    public string Emissora { get; set; } = null!;
    public string Cidade { get; set; } = null!;
}
namespace PortalGtf.Application.ViewModels.PostsVM;

public class PostListViewModel
{
    public int Id { get; set; }
    public string Titulo { get; set; } = null!;
    public string Conteudo { get; set; } = null!;
    public string Editorial { get; set; } = null!;
    public string Emissora { get; set; } = null!;
    public string? Imagem  { get; set; }
    public string Status { get; set; } = null!;
    public DateTime DataCriacao { get; set; }
    public DateTime? PublicadoEm { get; set; }
    public int UsuarioCriacaoId { get; set; }
    public string UsuarioCriacao { get; set; }
    public string Cidade { get; set; } = null!;
}

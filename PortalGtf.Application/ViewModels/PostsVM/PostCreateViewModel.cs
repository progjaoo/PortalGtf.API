using PortalGtf.Application.ViewModels.MidiaVM;

namespace PortalGtf.Application.ViewModels.PostsVM;

public class PostCreateViewModel
{
    public string Titulo { get; set; } = null!;
    public string Subtitulo { get; set; } = null!;
    public string Conteudo { get; set; } = null!;
    public string Imagem { get; set; } = null!;
    public string Slug { get; set; } = null!;
    public int EditorialId { get; set; }
    public int? SubcategoriaId { get; set; }  
    public int EmissoraId { get; set; }
    public int CidadeId { get; set; }
    public int UsuarioCriacaoId { get; set; }
    public List<string> Tags { get; set; } = new();
    public List<MidiaViewModel> Midias { get; set; } = new();
}
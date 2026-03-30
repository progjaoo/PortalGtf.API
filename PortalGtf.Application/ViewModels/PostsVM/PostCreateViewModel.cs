using PortalGtf.Application.ViewModels.MidiaVM;
using PortalGtf.Core.Enums;

namespace PortalGtf.Application.ViewModels.PostsVM;

public class PostCreateViewModel
{
    public string Titulo { get; set; } = null!;
    public string Subtitulo { get; set; } = null!;
    public string Conteudo { get; set; } = null!;
    public int? ImagemCapaId { get; set; }
    public string Slug { get; set; } = null!;
    public int EditorialId { get; set; }
    public int? SubcategoriaId { get; set; }  
    public int EmissoraId { get; set; }
    public int CidadeId { get; set; }
    public int UsuarioCriacaoId { get; set; }
    public List<string> Tags { get; set; } = new();
}
public class PostMidiaInputViewModel
{
    public int MidiaId { get; set; }
    public int Ordem { get; set; }
    public TipoMidia Tipo { get; set; }
}
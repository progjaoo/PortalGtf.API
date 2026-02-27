using PortalGtf.Core.Entities;
using PortalGtf.Core.Enums;

namespace PortalGtf.Application.ViewModels.PostsVM;

public class PostDetailViewModel
{
    public int Id { get; set; }
    public string Titulo { get; set; } = null!;
    public string Subtitulo { get; set; } = null!;
    public string Conteudo { get; set; } = null!;
    public string Imagem { get; set; } = null!;
    public DateTime? PublicadoEm { get; set; }
    public DateTime? DataCriacao { get; set; }
    public StatusPost Status { get; set; }
    public int UsuarioCriacaoId { get; set; }
    public string UsuarioCriacao { get; set; }
    public string Editorial { get; set; } = null!;
    public string Subcategoria { get; set; } = null!;
    public string Emissora { get; set; } = null!;
    public string Cidade { get; set; } = null!;

    public List<string> Tags { get; set; } = new();
    
    public int TotalVisualizacoes { get; set; }
}

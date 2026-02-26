using PortalGtf.Core.Enums;

namespace PortalGtf.Application.ViewModels.PostsVM;

public class PostUpdateViewModel
{
    public string Titulo { get; set; } = null!;
    public string Subtitulo { get; set; } = null!;
    public string Conteudo { get; set; } = null!;
    public string Imagem { get; set; } = null!;
    public int EditorialId { get; set; }
    public StatusPost StatusPost { get; set; }
}

using PortalGtf.Core.Enums;

namespace PortalGtf.Application.ViewModels.MidiaVM;

public class MidiaDto
{
    public int Id { get; set; }
    public string NomeOriginal { get; set; } = null!;
    public string Url { get; set; } = null!;
    public TipoMidia Tipo { get; set; }       // 1 = Imagem, 2 = Video
    public string TipoLabel => Tipo == TipoMidia.Video ? "video" : "imagem";
    public DateTime DataUpload { get; set; }
    // Para vídeos, ThumbnailUrl pode ser null — o front trata com ícone
    public string? ThumbnailUrl { get; set; }
}
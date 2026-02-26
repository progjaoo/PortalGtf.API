namespace PortalGtf.Application.ViewModels.StreamingVM;

public class StreamingCreateViewModel
{
    public int EmissoraId { get; set; }
    public string Url { get; set; } = null!;
    public string Porta { get; set; } = null!;
    public string TipoStream { get; set; } = null!;
    public string? LinkApi { get; set; }
}
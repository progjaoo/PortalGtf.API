namespace PortalGtf.Application.ViewModels.TemaEditorialVM;

public class TemaEditorialViewModel
{
    public string? Descricao { get; set; }
    public string CorPrimaria { get; set; } = null!;
    public string CorSecundaria { get; set; } = null!;
    public string CorFonte { get; set; } = null!;
    public string Logo { get; set; } = null!;
}
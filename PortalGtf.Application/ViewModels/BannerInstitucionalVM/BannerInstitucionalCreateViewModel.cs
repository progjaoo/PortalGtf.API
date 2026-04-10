namespace PortalGtf.Application.ViewModels.BannerInstitucionalVM;

public class BannerInstitucionalCreateViewModel
{
    public string Titulo { get; set; } = null!;
    public int EmissoraId { get; set; }
    public int MidiaId { get; set; }
    public string LinkUrl { get; set; } = null!;
    public bool NovaAba { get; set; }
    public string Posicao { get; set; } = "home";
    public int Ordem { get; set; }
    public bool Ativo { get; set; } = true;
}

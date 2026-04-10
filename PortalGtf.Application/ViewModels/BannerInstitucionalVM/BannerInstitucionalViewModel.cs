namespace PortalGtf.Application.ViewModels.BannerInstitucionalVM;

public class BannerInstitucionalViewModel
{
    public int Id { get; set; }
    public string Titulo { get; set; } = null!;
    public int EmissoraId { get; set; }
    public string EmissoraNome { get; set; } = null!;
    public int MidiaId { get; set; }
    public string MidiaUrl { get; set; } = null!;
    public string LinkUrl { get; set; } = null!;
    public bool NovaAba { get; set; }
    public string Posicao { get; set; } = null!;
    public int Ordem { get; set; }
    public bool Ativo { get; set; }
    public DateTime DataCriacao { get; set; }
}

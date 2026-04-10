namespace PortalGtf.Core.Entities;

public class BannerInstitucional
{
    public int Id { get; set; }
    public string Titulo { get; set; } = null!;
    public int EmissoraId { get; set; }
    public Emissora Emissora { get; set; } = null!;
    public int MidiaId { get; set; }
    public Midia Midia { get; set; } = null!;
    public string LinkUrl { get; set; } = null!;
    public bool NovaAba { get; set; }
    public string Posicao { get; set; } = "home";
    public int Ordem { get; set; }
    public bool Ativo { get; set; }
    public DateTime DataCriacao { get; set; }
}

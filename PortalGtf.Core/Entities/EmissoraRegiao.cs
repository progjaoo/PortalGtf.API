namespace PortalGtf.Core.Entities;

public class EmissoraRegiao
{
    public int Id { get; set; }
    public int EmissoraId { get; set; }
    public Emissora Emissora { get; set; } = null!;
    public int RegiaoId { get; set; }
    public Regiao Regiao { get; set; } = null!;
}
namespace PortalGtf.Core.Entities;

public class Streaming
{
    public int Id { get; set; }
    public int EmissoraId { get; set; }
    public Emissora Emissora { get; set; } = null!;
    public string Url { get; set; } = null!;
    public string Porta { get; set; } = null!;
    public string TipoStream { get; set; } = null!;
    public string? LinkApi { get; set; }
}
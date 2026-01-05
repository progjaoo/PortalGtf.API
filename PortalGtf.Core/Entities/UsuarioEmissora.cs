namespace PortalGtf.Core.Entities;

public class UsuarioEmissora
{
    public int Id { get; set; }

    public int UsuarioId { get; set; }
    public Usuario Usuario { get; set; } = null!;

    public int EmissoraId { get; set; }
    public Emissora Emissora { get; set; } = null!;

    public int FuncaoId { get; set; }
    public Funcao Funcao { get; set; } = null!;
}
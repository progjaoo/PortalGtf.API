namespace PortalGtf.Core.Entities;

public class Regiao
{
    public int Id { get; set; }
    public string Nome { get; set; } = null!;
    public ICollection<Estado> Estados { get; set; } = new List<Estado>();
    public ICollection<EmissoraRegiao> EmissoraRegioes { get; set; } = new List<EmissoraRegiao>();
}

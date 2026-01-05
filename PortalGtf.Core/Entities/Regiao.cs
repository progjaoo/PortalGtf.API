namespace PortalGtf.Core.Entities;

public class Regiao
{
    public int Id { get; set; }
    public string Nome { get; set; } = null!;

    public ICollection<Cidade> Cidades { get; set; } = new List<Cidade>();
    public ICollection<EmissoraRegiao> EmissoraRegioes { get; set; } = new List<EmissoraRegiao>();

}

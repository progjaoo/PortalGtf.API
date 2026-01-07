namespace PortalGtf.Core.Entities;

public class Cidade
{
    public int Id { get; set; }
    public string Nome { get; set; } = null!;
    public int EstadoId { get; set; }
    public Estado Estado { get; set; } = null!;
    public int? RegiaoId { get; set; }
    public Regiao? Regiao { get; set; }
    public ICollection<Post> Posts { get; set; } = new List<Post>();
}
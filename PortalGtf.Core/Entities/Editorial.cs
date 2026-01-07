namespace PortalGtf.Core.Entities;

public class Editorial
{
    public int Id { get; set; }

    public string TipoPostagem { get; set; } = null!;
    
    public int TemaEditorialId { get; set; }

    public TemaEditorial TemaEditorial { get; set; } = null!;

    public ICollection<Post> Posts { get; set; } = new List<Post>();
}
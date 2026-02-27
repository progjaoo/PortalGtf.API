namespace PortalGtf.Core.Entities;

public class Subcategoria
{
    public int Id { get; set; }
    public string Nome { get; set; } = null!;
    public string Slug { get; set; } = null!;
    public int EditorialId { get; set; } 
    public Editorial Editorial { get; set; } = null!;
    public ICollection<Post> Posts { get; set; } = new List<Post>();
}
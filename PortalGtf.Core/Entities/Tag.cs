namespace PortalGtf.Core.Entities;

public class Tag
{
    public int Id { get; set; }
    public string Nome { get; set; } = null!;
    public string Slug { get; set; } = null!;

    public ICollection<PostTag> PostTags { get; set; } = new List<PostTag>();
}
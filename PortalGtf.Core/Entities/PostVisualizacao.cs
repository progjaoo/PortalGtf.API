namespace PortalGtf.Core.Entities;

public class PostVisualizacao
{
    public int Id { get; set; }
    public int PostId { get; set; }
    public Post Post { get; set; } = null!;
    public string? Ip { get; set; }
    public DateTime DataVisualizacao { get; set; }
}
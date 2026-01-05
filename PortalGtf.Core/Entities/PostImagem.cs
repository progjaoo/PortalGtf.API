namespace PortalGtf.Core.Entities;

public class PostImagem
{
    public int Id { get; set; }
    public int PostId { get; set; }
    public Post Post { get; set; } = null!;
    public string UrlPost { get; set; } = null!;
    public int Ordem { get; set; }
}
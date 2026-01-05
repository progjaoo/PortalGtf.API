namespace PortalGtf.Core.Entities;

public class TemaEditorial
{
    public int Id { get; set; }
    public string Descricao { get; set; } = null!;
    public string CorPrimaria { get; set; } = null!;
    public string CorSecundaria { get; set; } = null!;
    public string CorFonte { get; set; } = null!;
    public string Logo { get; set; } = null!;

    public ICollection<Editorial> Editoriais { get; set; } = new List<Editorial>();
}
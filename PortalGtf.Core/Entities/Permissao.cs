namespace PortalGtf.Core.Entities;

public class Permissao
{
    public int Id { get; set; }
    public string TipoPermissao { get; set; } = null!;

    public ICollection<FuncaoPermissao> FuncaoPermissoes { get; set; } = new List<FuncaoPermissao>();
}
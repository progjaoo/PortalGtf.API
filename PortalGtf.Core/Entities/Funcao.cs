namespace PortalGtf.Core.Entities;

public class Funcao
{
    public int Id { get; set; }
    public string TipoFuncao { get; set; } = null!;
    public ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
    public ICollection<FuncaoPermissao> FuncaoPermissoes { get; set; } = new List<FuncaoPermissao>();
}
namespace PortalGtf.Core.Entities;

public class FuncaoPermissao
{
    public int Id { get; set; }

    public int FuncaoId { get; set; }
    public Funcao Funcao { get; set; } = null!;

    public int PermissaoId { get; set; }
    public Permissao Permissao { get; set; } = null!;
}
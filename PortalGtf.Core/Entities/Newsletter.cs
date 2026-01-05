namespace PortalGtf.Core.Entities;

public class Newsletter
{
    public int Id { get; set; }
    public string Email { get; set; } = null!;
    public DateTime DataCadastro { get; set; }
}
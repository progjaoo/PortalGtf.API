namespace PortalGtf.Core.Entities;

public class Emissora
{
    public int Id { get; set; }
    public string NomeSocial { get; set; } = null!;
    public string RazaoSocial { get; set; } = null!;
    public string Cep { get; set; } = null!;
    public string Endereco { get; set; } = null!;
    public string Numero { get; set; } = null!;
    public string Bairro { get; set; } = null!;
    public string Estado { get; set; } = null!;
    public string Cidade { get; set; } = null!;
    public string Slug { get; set; } = null!;
    public string Logo { get; set; } = null!;
    public string LogoSmall { get; set; } = null!;
    public string TemaPrincipal { get; set; } = null!;
    public bool Ativa { get; set; }
    public ICollection<Post> Posts { get; set; } = new List<Post>();
    public ICollection<UsuarioEmissora> UsuarioEmissoras { get; set; } = new List<UsuarioEmissora>();
    public ICollection<Streaming> Streamings { get; set; } = new List<Streaming>();
    public ICollection<EmissoraRegiao> EmissoraRegioes { get; set; } = new List<EmissoraRegiao>();
}
namespace PortalGtf.Application.ViewModels.EmissoraVM;

public class EmissoraViewModel
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
    public bool Ativa { get; set; }
}
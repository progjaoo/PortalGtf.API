namespace PortalGtf.Application.ViewModels.CidadeVM;

public class CidadeViewModel
{
    public int Id { get; set; }
    public string Nome { get; set; } = null!;

    public int EstadoId { get; set; }
    public string EstadoNome { get; set; } = null!;

    public int? RegiaoId { get; set; }
    public string? RegiaoNome { get; set; }
}
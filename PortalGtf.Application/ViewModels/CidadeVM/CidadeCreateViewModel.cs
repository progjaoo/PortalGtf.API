using System.ComponentModel.DataAnnotations;

namespace PortalGtf.Application.ViewModels.CidadeVM;

public class CidadeCreateViewModel
{
    [Required]
    public string Nome { get; set; } = null!;
    [Required]
    public int EstadoId { get; set; }
    public int? RegiaoId { get; set; }
}
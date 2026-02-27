using PortalGtf.Core.Entities;
using PortalGtf.Core.Enums;

namespace PortalGtf.Application.ViewModels.MidiaVM;

public class MidiaViewModel
{
    public string Url { get; set; } = null;
    public int Ordem { get; set; }
    public TipoMidia Tipo { get; set; }
    public int MidiaId { get; set; }
    public Midia Midia { get; set; }
}
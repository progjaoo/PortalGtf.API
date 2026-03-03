using PortalGtf.Core.Enums;

namespace PortalGtf.Application.ViewModels.PostsVM;

public class PostEnumViewModel
{
    public FiltroPostEnum FiltroData { get; set; } = FiltroPostEnum.QualquerData;
    public OrdenaPostEnum OrdenarPor { get; set; } = OrdenaPostEnum.MaisRecente;
}
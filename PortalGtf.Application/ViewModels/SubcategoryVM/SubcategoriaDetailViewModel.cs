namespace PortalGtf.Application.ViewModels.SubcategoryVM;

public class SubcategoriaDetailViewModel
{
    public int Id { get; set; }
    public string Nome { get; set; } = null!;
    public string Slug { get; set; } = null!;
    public int EditorialId { get; set; }
}
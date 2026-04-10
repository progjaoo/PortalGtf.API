namespace PortalGtf.Application.ViewModels.EditorialVM;

public class EditorialViewModel
{
    public int Id { get; set; }
    public string TipoPostagem { get; set; } = null!;
    public int TemaEditorialId { get; set; }
    public int EmissoraId { get; set; }
    public string? EmissoraNome { get; set; }
}

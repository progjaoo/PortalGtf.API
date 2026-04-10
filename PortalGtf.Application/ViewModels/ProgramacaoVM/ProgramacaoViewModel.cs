namespace PortalGtf.Application.ViewModels.ProgramacaoVM;

public class ProgramacaoRadioViewModel
{
    public int Id { get; set; }
    public int EmissoraId { get; set; }
    public string? EmissoraNome { get; set; }
    public string NomePrograma { get; set; }
    public string? Apresentador { get; set; }
    public string? Descricao { get; set; }
    public int DiaSemana { get; set; }
    public TimeSpan HoraInicio { get; set; }
    public TimeSpan HoraFim { get; set; }
    public string? Imagem { get; set; }
    public bool Ativo { get; set; }

}
